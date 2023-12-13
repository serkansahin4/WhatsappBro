using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.UserFriendMaps
{
    public class UserFriendMap : Profile
    {
        public UserFriendMap()
        {
            CreateMap<UserFriend, UserFriendDetailDto>().ReverseMap();
        }
    }
}
