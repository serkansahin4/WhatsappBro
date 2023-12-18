using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserFriendDetailDto:IDto
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public UserDetailDto User { get; set; }
        public UserDetailDto Friend { get; set; }
    }
}
