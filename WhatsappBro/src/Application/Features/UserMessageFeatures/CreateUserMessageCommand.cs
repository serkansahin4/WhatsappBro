using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.UserMessageFeatures
{
    public class CreateUserMessageCommand:IRequest<Guid>
    {
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
        public string? RootPath { get; set; }
    }
}
