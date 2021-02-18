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

        public int Width { get; set; }
        public int Height { get; set; }
        public async Task<byte[]> ImageResize(IBrowserFile imageFile)
        {
            await using var memoryStream = new MemoryStream();
            using var imageCropTask = SixLabors.ImageSharp.Image.LoadAsync(imageFile.OpenReadStream(long.MaxValue));
            

            var imageCrop = await imageCropTask;
           
                imageCrop.Mutate(x => x
                    .Resize(new ResizeOptions(){
                        Size = new Size(Width,Height),
                        Mode = ResizeMode.Pad
                        }));
           

            await imageCrop.SaveAsync(memoryStream, new JpegEncoder());

            var buffer = memoryStream.ToArray();
            return buffer;
        }
    }
}
