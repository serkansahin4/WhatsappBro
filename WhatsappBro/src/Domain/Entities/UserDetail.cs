using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UserDetails")]
    public class UserDetail:BaseEntity<Guid>
    {
        public string ImagePath { get; set; }
        public string ThumnailImagePath { get; set; }
        public AppUser User { get; set; }
    }
}
