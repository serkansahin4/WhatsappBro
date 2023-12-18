using Application.DTOs;
using Application.Features.UserMessageFeatures;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.MessageMaps
{
    public class MessageMap : Profile
    {
        public MessageMap()
        {
            CreateMap<CreateUserMessageCommand, UserMessage>();

            CreateMap<UserMessageDto, UserMessage>().ReverseMap();
        }
    }
}
