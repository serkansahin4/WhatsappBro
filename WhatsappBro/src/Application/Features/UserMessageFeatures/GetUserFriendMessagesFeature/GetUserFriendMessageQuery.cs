using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserFriendMessagesFeature
{
    public class GetUserFriendMessageQuery:IRequest<List<UserFriendMessageDto>>
    {
        public string UserName { get; set; }
        public string RootPath { get; set; }
    }
}
