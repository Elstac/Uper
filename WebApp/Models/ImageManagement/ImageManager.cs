namespace WebApp.Models.ImageManagement
{
    public interface IImageManager
    {
        void RemoveImage(string id, string directory);
        void SaveImage(string id, string directory);
    }

    public class ImageManager:IImageManager
    {
        private IImageSaver imageSaver;
        private IImageRemover imageRemover;

        public ImageManager(IImageSaver imageSaver, IImageRemover imageRemover)
        {
            this.imageSaver = imageSaver;
            this.imageRemover = imageRemover;
        }

        public void RemoveImage(string id, string directory)
        {
            throw new System.NotImplementedException();
        }

        public void SaveImage(string id, string directory)
        {
            throw new System.NotImplementedException();
        }
    }
}
