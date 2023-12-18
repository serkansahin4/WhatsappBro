using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures.GetUserMessageFeature
{
    public class GetUserMessageDetailQuery:IRequest<List<UserMessageDto>>
    {
        public string UserName { get; set; }
    }
}
