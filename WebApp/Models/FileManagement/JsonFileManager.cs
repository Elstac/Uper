using WebApp.Models.Factories;

namespace WebApp.Models.FileManagement
{
    public class JsonFileManager : FileManager
    {
        public JsonFileManager(IFileSaverFactory fileSaverFactory, IFileRemover fileRemover):
            base(fileSaverFactory.GetSaver(SaverType.Text),fileRemover)
        {

        }

        public override void RemoveFile(string id, string directory)
        {
            fileRemover.RemoveImage(id, directory, ".json");
        }

        public override string SaveFile(string fileData, string directory)
        {
            return fileSaver.SaveFile(fileData, ".json", directory);
        }
    }
}
