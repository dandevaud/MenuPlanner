using System;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Http;

namespace MenuPlanner.Server.Contracts.Blob
{
    public interface IPictureHandler
    {
        public Task<string> SaveImage(Guid menuId, Image image);

        public bool ImageExists(Guid menuId, Image image);

        /// <summary>Deletes the image.</summary>
        /// <param name="menuId">The menu identifier.</param>
        /// <param name="imageId">The image identifier, if null or empty all images of the menu are deleted</param>
        public bool DeleteImage(Guid menuId, Guid imageId);
    }
}
