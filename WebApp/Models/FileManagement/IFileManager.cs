namespace WebApp.Models.FileManagement
{
    public interface IFileManager
    {
        void RemoveFile(string id,string directory);
        string SaveFile(string fileData, string directory);
    }

    public abstract class FileManager : IFileManager
    {
        protected IFileSaver fileSaver;
        protected IFileRemover fileRemover;

        public FileManager(IFileSaver fileSaver, IFileRemover fileRemover)
        {
            this.fileSaver = fileSaver;
            this.fileRemover = fileRemover;
        }

        public abstract void RemoveFile(string id, string directory);

        public abstract string SaveFile(string fileData, string directory);
    }
}
