using System;

namespace WebApp.Models
{
    public interface IImageWriter
    {
        string SaveImage(string imageName, string imageData);
    }

    public class ImageWriter : IImageWriter
    {
        public string SaveImage(string imageName, string imageData)
        {
            throw new NotImplementedException();
        }
    }
}
