using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserFriendMessageDto : IDto
    {
        public string FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendProfilePhotoBase64Thumbnail { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageDateTime => MessageDate.ToShortTimeString();
    }
}
