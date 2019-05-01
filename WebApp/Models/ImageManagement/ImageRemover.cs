using System;
using System.IO;

namespace WebApp.Models.ImageManagement
{
    public interface IImageRemover
    {
        void RemoveImage(string imageId, string directory,string fileExtention);
    }
    public class ImageRemover : IImageRemover
    {
        public void RemoveImage(string imageId, string directory, string fileExtention)
        {
            var name = directory + imageId + fileExtention;

            File.Delete(name);
        }
    }
}
