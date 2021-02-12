// <copyright file="MenuImages.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        private int done = 0;
        private int total = 0;

        private static readonly decimal RATIO = new decimal(0.75d);



        private IReadOnlyList<IBrowserFile> selectedFiles;
        private string filesMessage = "No file(s) selected";

        private async void OnInputFileChange(InputFileChangeEventArgs e)
        {
            if (Menu.Images == null)
            {
                Menu.Images = new List<Image>();
            }
            var maxFiles = 10;

            selectedFiles = e.GetMultipleFiles(maxFiles);
            total = selectedFiles.Count;
            try
            {
                IsLoading = true;
               
                foreach (var imageFile in selectedFiles)
                {

                    var buffer = await ImageResize(imageFile);

                    Image image = new Image
                    {
                        ImageBytes = buffer,
                        AlternativeName = imageFile.Name,
                        Name = imageFile.Name
                    };

                    Menu.Images.Add(image);
                    done++;
                    StateHasChanged();
                }

            }
            finally
            {
                IsLoading = false;
                
              
            }

            filesMessage = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
        }

        private async Task<byte[]> ImageResize(IBrowserFile imageFile)
        {
            using var imageCrop = await SixLabors.ImageSharp.Image.LoadAsync(imageFile.OpenReadStream());
            await using var memoryStream = new MemoryStream();

           
          

            var ratio = (new decimal(imageCrop.Height) / new decimal(imageCrop.Width)) > RATIO
                        && (new decimal(imageCrop.Width) / new decimal(imageCrop.Height)) > RATIO;

            if (ratio)
            {
              
                imageCrop.Mutate(x => x
                    .Resize(400, 400));
            }
            else
            {
                imageCrop.Mutate( x => x.Resize(new ResizeOptions()
                {
                    Size = new Size(400),
                    Mode = ResizeMode.Min
                }));
               var min = imageCrop.Width > imageCrop.Height ? imageCrop.Height : imageCrop.Width;
               var center = Rectangle.Center(imageCrop.Frames.RootFrame.Bounds());
               if (min % 2 == 1) min--;
                center.X -= min/2;
                center.Y -= min / 2;

                imageCrop.Mutate(x => x
                        .Crop(
                            new Rectangle(center, 
                                new Size(min, min))));

            }

            await imageCrop.SaveAsync(memoryStream, new JpegEncoder());

            var buffer = memoryStream.ToArray();
            return buffer;
        }

        private bool isNew(Image image)
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
