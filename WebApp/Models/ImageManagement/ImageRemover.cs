using System;

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
            throw new NotImplementedException();
        }
    }
}
