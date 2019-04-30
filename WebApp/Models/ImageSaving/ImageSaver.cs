using System;

namespace WebApp.Models
{
    public interface IImageSaver
    {
        string SaveImage(string imageData, string fileExtention, string directory);
    }
    public class ImageSaver : IImageSaver
    {
        private IFileIdProvider idProvider;
        private IImageWriter imageWriter;

        public ImageSaver(IFileIdProvider idProvider, IImageWriter imageWriter)
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
