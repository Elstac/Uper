using System;

namespace WebApp.Models
{
    public interface IImageSaver
    {
        string SaveImage(string imageName);
    }
    public class ImageSaver : IImageSaver
    {
        public string SaveImage(string imageName)
        {
            throw new NotImplementedException();
        }
    }
}
