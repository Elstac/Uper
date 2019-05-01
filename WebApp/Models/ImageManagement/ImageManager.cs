namespace WebApp.Models.ImageManagement
{
    public interface IImageManager
    {
        void RemoveImage(string id,string directory);
        string SaveImage(string imageData, string directory);
    }

    public class PngImageManager:IImageManager
    {
        private IImageSaver imageSaver;
        private IImageRemover imageRemover;

        public PngImageManager(IImageSaver imageSaver, IImageRemover imageRemover)
        {
            this.imageSaver = imageSaver;
            this.imageRemover = imageRemover;
        }

        public void RemoveImage(string id, string directory)
        {
            throw new System.NotImplementedException();
        }

        public string SaveImage(string imageData, string directory)
        {
            throw new System.NotImplementedException();
        }
    }
}
