using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserFriendMessageByIdFeature
{
    public class GetUserFriendMessageByIdQuery:IRequest<List<UserMessageDto>>
    {
        public string FriendId { get; set; }
        public string RootPath { get; set; }
    }
}
