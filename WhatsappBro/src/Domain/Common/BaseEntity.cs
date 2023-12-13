using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
