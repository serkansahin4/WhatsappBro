using Application.DTOs;
using Application.Features.UserFriendFeatures.GetUserFriendDetails;
using Application.Features.UserMessageFeatures.GetUserMessageFeature;
using Core.Helpers.FileHelper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserFriendMessagesFeature
{
    public class GetUserFriendMessageQueryHandler : IRequestHandler<GetUserFriendMessageQuery, List<UserFriendMessageDto>>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public GetUserFriendMessageQueryHandler(IMediator mediator, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<List<UserFriendMessageDto>> Handle(GetUserFriendMessageQuery request, CancellationToken cancellationToken)
        {
            Guid connectedUserId = _userManager.Users.SingleOrDefault(x => x.UserName == request.UserName).Id;

            List<UserFriendDetailDto> userFriends = await _mediator.Send(new GetUserFriendDetailQuery
            {
                UserName = request.UserName
            });

            List<UserMessageDto> userMessages = await _mediator.Send(new GetUserMessageDetailQuery
            {
                UserName = request.UserName
            });

            List<UserFriendMessageDto> userFriendMessages = new List<UserFriendMessageDto>();

            foreach (UserFriendDetailDto item in userFriends)
            {
                UserMessageDto message = userMessages.Where(x => (x.ReceiverUser.UserName == item.User.UserName || x.SenderUser.UserName == item.User.UserName)
                    && (x.ReceiverUser.UserName == item.Friend.UserName || x.SenderUser.UserName == item.Friend.UserName)).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
                if (message == null)
                {
                    continue;
                }

                string base64string = connectedUserId == item.FriendId ? FileHelper.ConvertToBase64(Path.Combine(request.RootPath, item.User.ThumbnailPath)) : FileHelper.ConvertToBase64(Path.Combine(request.RootPath, item.Friend.ThumbnailPath));
                userFriendMessages.Add(new UserFriendMessageDto
                {
                    FriendId = connectedUserId == item.FriendId ? item.UserId.ToString() : item.FriendId.ToString(),
                    FriendName = connectedUserId == item.FriendId ? item.User.UserName : item.Friend.UserName,
                    Message = new String(message.Message.Take(30).ToArray()),
                    MessageDate = message.CreatedDate,
                    FriendProfilePhotoBase64Thumbnail = base64string
                });
            }

            return userFriendMessages;
        }
    }
}
