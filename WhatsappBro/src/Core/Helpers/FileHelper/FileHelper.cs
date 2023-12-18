using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.FileHelper
{
    public static class FileHelper
    {
        public static string ConvertToBase64(string filePath)
        {
            using FileStream fileStream = new FileStream(filePath,FileMode.Open);
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        public async static Task<ImageResponse> CreateAsync(IFormFile file, string filePath, string thumbnailPath, int maxFileSize, string[] extensions)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            bool extensionControl = extensions.Contains(ext);

            if (file.Length < maxFileSize)
            {
                if (ext != null && extensionControl)
                {
                    Guid imageId = Guid.NewGuid();
                    string fileName = imageId + ext;

                    filePath = filePath + fileName;
                    thumbnailPath = thumbnailPath + fileName;
                }
                using (FileStream uploadStream = File.Create($@"{Environment.CurrentDirectory}\\wwwroot\\{filePath}"))
                {
                    await file.CopyToAsync(uploadStream);
                    await uploadStream.FlushAsync();
                }
                using (FileStream uploadStream = File.Create($@"{Environment.CurrentDirectory}\\wwwroot\\{thumbnailPath}"))
                {
                    await file.CopyToAsync(uploadStream);
                    await uploadStream.FlushAsync();
                }
                return new ImageResponse { IsSuccess = true, Path = filePath, ThumbnailPath = thumbnailPath };
            }
            return new ImageResponse { IsSuccess = false };
        }
    }
}
