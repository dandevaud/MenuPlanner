using System;
using System.IO;
using System.Threading.Tasks;
using MenuPlanner.Client.Controls.MenuControls;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace MenuPlanner.Client.Logic
{
    public class ImageResizer
    {

        public decimal Ratio { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public async Task<byte[]> ImageResize(IBrowserFile imageFile)
        {
            await using var memoryStream = new MemoryStream();
            using var imageCropTask = SixLabors.ImageSharp.Image.LoadAsync(imageFile.OpenReadStream(long.MaxValue));
            

            var imageCrop = await imageCropTask;
            var ratio = (new decimal(imageCrop.Height) / new decimal(imageCrop.Width)) > Ratio
                        && (new decimal(imageCrop.Width) / new decimal(imageCrop.Height)) > Ratio;

            if (ratio)
            {
              
                imageCrop.Mutate(x => x
                    .Resize(Width, Height));
            }
            else
            {
                imageCrop.Mutate( x => x.Resize(new ResizeOptions()
                {
                    Size = new Size(Math.Min(Width,Height)),
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
    }
}
