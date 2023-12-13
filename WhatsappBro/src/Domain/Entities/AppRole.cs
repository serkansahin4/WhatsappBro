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
    [Table("Roles")]
    public class AppRole : IdentityRole<Guid>, IEntity
    {
        public DateTime CreatedDate { get; set; }
    }
}
