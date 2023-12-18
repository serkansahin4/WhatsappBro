using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserFriendMessageByMessageIdFeature
{
    public class GetUserFriendMessageByMessageIdQuery:IRequest<UserFriendMessageDto>
    {
        public string MessageId { get; set; }
        public string RootPath { get; set; }
    }
}
