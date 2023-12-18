using Application.DTOs;
using Application.Hubs;
using Application.Interfaces;
using AutoMapper;
using Core.Helpers.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures
{
    public class CreateUserMessageCommandHandler : IRequestHandler<CreateUserMessageCommand, Guid>
    {
        readonly IHubContext<ChatHub> _chatHub;
        readonly IMapper _mapper;
        readonly UserManager<AppUser> _userManager;
        readonly IUserMessageRepository _userMessageRepository;
        readonly IHttpContextAccessor _contextAccessor;
        public CreateUserMessageCommandHandler(IHubContext<ChatHub> chatHub, IMapper mapper, UserManager<AppUser> userManager, IUserMessageRepository userMessageRepository, IHttpContextAccessor contextAccessor)
        {
            _chatHub = chatHub;
            _mapper = mapper;
            _userManager = userManager;
            _userMessageRepository = userMessageRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<Guid> Handle(CreateUserMessageCommand request, CancellationToken cancellationToken)
        {
            string authUserName = _contextAccessor.HttpContext.User.Identity.Name;
            IEnumerable<AppUser> users = _userManager.Users.Where(x => x.UserName == authUserName || x.Id == Guid.Parse(request.ReceiverUserId)).AsEnumerable();
            AppUser senderUser = users.SingleOrDefault(x => x.UserName == authUserName);
            AppUser receiverUser = users.SingleOrDefault(x => x.Id == Guid.Parse(request.ReceiverUserId));
            UserMessage userMessage = new UserMessage
            {
                Id = Guid.NewGuid(),
                SenderId = senderUser.Id,
                ReceiverId = receiverUser.Id,
                Message = request.Message,
                CreatedDate = DateTime.Now
            };

            await _userMessageRepository.InsertAsync(userMessage);
            await _userMessageRepository.SaveChangesAsync();

            string base64string = FileHelper.ConvertToBase64(Path.Combine(request.RootPath, senderUser.ThumbnailPath));

            string receiveConnectionId = ChatHubClient.chatClients.SingleOrDefault(x => x.UserName == receiverUser.UserName)?.ConnectionId;
            if (!string.IsNullOrEmpty(receiveConnectionId))
            {
                UserFriendMessageDto userFriendMessageDto = new UserFriendMessageDto
                {
                    FriendId = senderUser.Id.ToString(),
                    FriendName = senderUser.UserName,
                    Message = request.Message,
                    MessageDate = userMessage.CreatedDate,
                    FriendProfilePhotoBase64Thumbnail = base64string
                };

                await _chatHub.Clients.Client(receiveConnectionId).SendAsync("receiveMessage", userFriendMessageDto);
            }

            
            return userMessage.Id;
        }
    }
}
