using System;
using System.IO;

namespace WebApp.Models.ImageManagement
{
    public interface IImageWriter
    {
        void SaveImage(string imageName, string imageData);
    }

    public class ImageWriter : IImageWriter
    {
        public void SaveImage(string imageName, string imageData)
        {
            using (var fs = new FileStream(imageName, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }
    }
}
