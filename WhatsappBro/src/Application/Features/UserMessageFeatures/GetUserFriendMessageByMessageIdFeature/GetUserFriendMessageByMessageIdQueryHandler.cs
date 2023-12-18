using Application.DTOs;
using Application.Interfaces;
using Core.Helpers.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserFriendMessageByMessageIdFeature
{
    public class GetUserFriendMessageByMessageIdQueryHandler : IRequestHandler<GetUserFriendMessageByMessageIdQuery, UserFriendMessageDto>
    {
        readonly IUserMessageRepository _messageRepository;
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContext;
        public GetUserFriendMessageByMessageIdQueryHandler(IUserMessageRepository messageRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContext)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public async Task<UserFriendMessageDto> Handle(GetUserFriendMessageByMessageIdQuery request, CancellationToken cancellationToken)
        {
            string authUserName = _httpContext.HttpContext.User.Identity.Name;
            Guid authUserId = _userManager.Users.SingleOrDefault(x => x.UserName == authUserName).Id;

            UserMessage userMessage = await _messageRepository.GetAsync(x => x.Id == Guid.Parse(request.MessageId));

            AppUser senderUser = _userManager.Users.SingleOrDefault(x => x.Id == userMessage.SenderId);
            AppUser receiverUser = _userManager.Users.SingleOrDefault(x => x.Id == userMessage.ReceiverId);

            string base64string = userMessage.SenderId == authUserId ? FileHelper.ConvertToBase64(Path.Combine(request.RootPath, receiverUser.ThumbnailPath)) : FileHelper.ConvertToBase64(Path.Combine(request.RootPath, senderUser.ThumbnailPath));

            UserFriendMessageDto userFriendMessageDto = new UserFriendMessageDto
            {
                FriendId = userMessage.SenderId == authUserId ? userMessage.ReceiverId.ToString() : userMessage.SenderId.ToString(),
                FriendName = userMessage.SenderId == authUserId ? receiverUser.UserName : senderUser.UserName,
                Message = userMessage.Message,
                MessageDate = userMessage.CreatedDate,
                FriendProfilePhotoBase64Thumbnail = base64string
            };
            return userFriendMessageDto;
        }
    }
}
