namespace WebApp.Models.FileManagement
{
    public interface IFileManager
    {
        void RemoveFile(string id,string directory);
        string SaveFile(string fileData, string directory);
    }

    public class JsonImageManager:IFileManager
    {
        private IFileSaver fileSaver;
        private IFileRemover imageRemover;

        public JsonImageManager(IFileSaver fileSaver, IFileRemover imageRemover)
        {
            this.fileSaver = fileSaver;
            this.imageRemover = imageRemover;
        }


        public void RemoveFile(string id, string directory)
        {
            imageRemover.RemoveImage(id, directory, ".json");
        }

        public string SaveFile(string fileData, string directory)
        {
            return fileSaver.SaveImage(fileData, ".json", directory);
        }
    }
}
