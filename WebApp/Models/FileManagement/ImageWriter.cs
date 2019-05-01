using System;
using System.IO;

namespace WebApp.Models.FileManagement
{
    public class ImageWriter : IFileWriter
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
