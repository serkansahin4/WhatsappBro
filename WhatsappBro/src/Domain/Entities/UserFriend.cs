using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UserFriends")]
    public class UserFriend : IEntity
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public AppUser User { get; set; } //Arkadaşı olduğu
        public AppUser Friend { get; set; } //Arkadaşı olanlar
        public DateTime CreatedDate { get; set; }
    }
}
