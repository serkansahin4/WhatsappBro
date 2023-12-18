using Application.DTOs;
using Application.Features.UserFriendFeatures.GetUserFriendDetails;
using Application.Features.UserMessageFeatures.GetUserMessageFeature;
using Application.Interfaces;
using AutoMapper;
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

namespace Application.Features.UserMessageFeatures.GetUserFriendMessageByIdFeature
{
    public class GetUserFriendMessageByIdQueryHandler : IRequestHandler<GetUserFriendMessageByIdQuery, List<UserMessageDto>>
    {
        readonly IMapper _mapper;
        readonly IHttpContextAccessor _contextAccessor;

        readonly UserManager<AppUser> _userManager;
        readonly IUserMessageRepository _messageRepository;

        public GetUserFriendMessageByIdQueryHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserMessageRepository messageRepository, IMapper mapper)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<UserMessageDto>> Handle(GetUserFriendMessageByIdQuery request, CancellationToken cancellationToken)
        {
            string connectedUserName = _contextAccessor.HttpContext.User.Identity.Name;
            Guid connectedUserId = _userManager.Users.SingleOrDefault(x => x.UserName == connectedUserName).Id;
            
            List<UserMessageDto> userMessages = 
                _mapper.Map<List<UserMessageDto>>(
                await _messageRepository.GetUserMessages(x => x.SenderId == Guid.Parse(request.FriendId) || x.ReceiverId == Guid.Parse(request.FriendId))
                ).ConvertAll(x =>
                {
                    x.SenderUser.ThumbnailBase64 = FileHelper.ConvertToBase64(Path.Combine(request.RootPath, x.SenderUser.ThumbnailPath));
                    x.ReceiverUser.ThumbnailBase64 = FileHelper.ConvertToBase64(Path.Combine(request.RootPath, x.ReceiverUser.ThumbnailPath));
                    return x;
                }).ToList();
            userMessages = userMessages.ConvertAll(x => { x.OwnerId = connectedUserId;return x; }).OrderBy(x=>x.CreatedDate).ToList();
            return userMessages;
        }
    }
}
