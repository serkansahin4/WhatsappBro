using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ThumbnailPath { get; set; }
        public string Path { get; set; }
        public string ThumbnailBase64 { get; set; }
    }
}
