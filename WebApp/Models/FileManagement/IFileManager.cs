namespace WebApp.Models.FileManagement
{
    public interface IFileManager
    {
        void RemoveFile(string id,string directory);
        string SaveFile(string fileData, string directory);
        string ReadFile(string id, string directory);
    }

    public class JsonImageManager:IFileManager
    {
        private IFileSaver fileSaver;
        private IFileRemover imageRemover;
        private IFileReader<string> fileReader;

        public JsonImageManager(IFileSaver fileSaver, IFileRemover imageRemover, IFileReader<string> fileReader)
        {
            this.fileSaver = fileSaver;
            this.imageRemover = imageRemover;
            this.fileReader = fileReader;
        }

        public string ReadFile(string id, string directory)
        {
            return fileReader.ReadFileContent(directory + id + ".json");
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
