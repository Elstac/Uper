using System;

namespace WebApp.Models.ImageManagement
{
    public interface IImageRemover
    {
        void RemoveImage(string imageId, string directory);
    }
    public class ImageRemover : IImageRemover
    {
        public void RemoveImage(string imageId, string directory)
        {
            throw new NotImplementedException();
        }
    }
}
