using Application.Features.UserFeatures;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.UserMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<InsertUserCommand, AppUser>();
        }
    }
}
