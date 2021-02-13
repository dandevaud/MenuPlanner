// <copyright file="MenuImages.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Client.Logic;
using MenuPlanner.Shared.Extension;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Image = MenuPlanner.Shared.models.Image;

namespace MenuPlanner.Client.Controls.MenuControls
{
    public partial class MenuImages
    {

        [Parameter]
        public Menu Menu { get; set; }

        private bool IsLoading { get; set; }

        private UInt16 done = 0;
        private UInt16 total = 0;

     

        private IReadOnlyList<IBrowserFile> selectedFiles;
        private string filesMessage = "No file(s) selected";
        private readonly ImageResizer _imageResizer = new ImageResizer()
        {
            Ratio = new decimal(0.75d),
            Width = 400,
            Height = 400
        };

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            if (Menu.Images == null)
            {
                Menu.Images = new List<Image>();
            }
            var maxFiles = 10;

            selectedFiles = e.GetMultipleFiles(maxFiles);
            total = (ushort) selectedFiles.Count;
            List<Image> images = new List<Image>();
            try
            {
                IsLoading = true;
               foreach (var imageFile in selectedFiles)
                {
                    images.Add(await HandleImage(imageFile));
                    
                }
               Menu.Images.AddRange(images);

                
            }
            finally
            {
                IsLoading = false;
                
              
            }
            filesMessage = $"{selectedFiles.Count} file(s) selected";
        }

        private async Task<Image> HandleImage(IBrowserFile imageFile)
        {
            var buffer = _imageResizer.ImageResize(imageFile);

            Image image = new Image
            {
                ImageBytes = await buffer,
                AlternativeName = imageFile.Name,
                Name = imageFile.Name
            };
            done++;
            StateHasChanged();
            return image;
           
           
        }

        private bool IsNew(Image image)
        {
            return image.ImageBytes.Length != 0;
        }

        private string GetImagePath(Image img)
        {
            return Path.Combine(NavigationManager.BaseUri, img.Path.ToString());
        }

        private void RemoveImage(Image image)
        {
            Menu.Images.Remove(image);
        }

    }
}
