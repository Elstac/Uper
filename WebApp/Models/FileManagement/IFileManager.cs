namespace WebApp.Models.FileManagement
{
    public interface IFileManager
    {
        void RemoveFile(string id,string directory);
        string SaveFile(string fileData, string directory);
    }

    public class PngImageManager:IFileManager
    {
        private IFileSaver imageSaver;
        private IFileRemover imageRemover;

        public PngImageManager(IFileSaver fileSaver, IFileRemover imageRemover)
        {
            this.imageSaver = fileSaver;
            this.imageRemover = imageRemover;
        }

        public void RemoveFile(string id, string directory)
        {
            imageRemover.RemoveImage(id, directory, ".png");
        }

        public string SaveFile(string fileData, string directory)
        {
            return imageSaver.SaveImage(fileData, ".png", directory);
        }
    }
}
