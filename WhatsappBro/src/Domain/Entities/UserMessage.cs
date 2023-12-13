using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UserMessages")]
    public class UserMessage : BaseEntity<Guid>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public AppUser SenderUser { get; set; }
        public AppUser ReceiverUser { get; set; }
    }
}
