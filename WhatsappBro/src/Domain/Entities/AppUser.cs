using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Users")]
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public DateTime CreatedDate { get; set; }
        public UserDetail UserDetail { get; set; }
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public List<UserFriend> UserFriends { get; set; } //Arkadaşı Olanlar
        public List<UserMessage> UserMessages { get; set; }
    }
}
