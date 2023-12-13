using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.GetUserFriends
{
    public class GetUserFriendsQuery:IRequest<List<UserFriendDetailDto>>
    {
        public Guid UserId { get; set; }
    }
}
