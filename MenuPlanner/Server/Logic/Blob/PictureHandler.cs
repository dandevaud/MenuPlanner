using System;
using System.IO;
using System.Threading.Tasks;
using MenuPlanner.Server.Contracts.Blob;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace MenuPlanner.Server.Logic.Blob
{
    public class PictureHandler : IPictureHandler
    {

        private IWebHostEnvironment _hostingEnvironment;
      
        public PictureHandler(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private string GetPath()
        {   
            return _hostingEnvironment.WebRootPath;
        }

        private string GetRelativePath(Guid menuId, Guid imageId)
        {
            if (imageId.Equals(Guid.Empty))
            {
                return Path.Combine("imageUploads", menuId.ToString());
            }
            else
            {
                return Path.Combine("imageUploads", menuId.ToString(), imageId.ToString());
            }
            
        }

        public async Task<string> SaveImage(Guid menuId, Image image)
        {
            var relativePath = GetRelativePath(menuId, image.Id);
            var path = Path.Combine(GetPath(), relativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using (var fileStream = System.IO.File.Create(Path.Combine(path, image.AlternativeName)))
            {
                await fileStream.WriteAsync(image.ImageBytes);
               
            }
            return Path.Combine(relativePath, image.AlternativeName);
        }

        public bool DeleteImage(Guid menuId, Guid imageId)
        {
           
           
              var path = Path.Combine(GetPath(), GetRelativePath(menuId, imageId));
            

            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ImageExists(Guid menuId, Image image)
        {
            
            return Directory.Exists(Path.Combine(GetPath(), GetRelativePath(menuId, image.Id)));
        }
    }
}
