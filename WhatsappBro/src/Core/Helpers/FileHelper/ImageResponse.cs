using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.FileHelper
{
    public class ImageResponse
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public bool IsSuccess { get; set; }
    }
}
