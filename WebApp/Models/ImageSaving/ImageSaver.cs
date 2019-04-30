using System;

namespace WebApp.Models
{
    public interface IImageSaver
    {
        string SaveImage(string imageData, string fileExtention, string directory);
    }
    public class ImageSaver : IImageSaver
    {
        private IImageIdProvider idProvider;
        private IImageWriter imageWriter;

        public ImageSaver(IImageIdProvider idProvider, IImageWriter imageWriter)
        {
            this.idProvider = idProvider;
            this.imageWriter = imageWriter;
        }

        public string SaveImage(string imageData, string fileExtention, string directory)
        {
            throw new NotImplementedException();
        }
    }
}
