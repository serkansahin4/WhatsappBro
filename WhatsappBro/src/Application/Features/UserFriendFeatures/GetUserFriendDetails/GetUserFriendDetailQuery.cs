using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.GetUserFriendDetails
{
    public class GetUserFriendDetailQuery:IRequest<List<UserFriendDetailDto>>
    {
        public string UserName { get; set; }
    }
}
