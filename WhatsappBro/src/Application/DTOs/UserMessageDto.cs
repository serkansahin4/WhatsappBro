using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserMessageDto
    {
        public Guid OwnerId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateTime => CreatedDate.ToShortTimeString();
        public UserDetailDto SenderUser { get; set; }
        public UserDetailDto ReceiverUser { get; set; }
    }
}
