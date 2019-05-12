using WebApp.Models.Factories;

namespace WebApp.Models.FileManagement
{
    public class PngFileManager : FileManager
    {
        public PngFileManager(IFileSaverFactory fileSaverFactory, IFileRemover fileRemover) :
            base(fileSaverFactory.GetSaver(SaverType.Image), fileRemover)
        {

        }

        public override void RemoveFile(string id, string directory)
        {
            fileRemover.RemoveImage(id, directory, ".png");
        }

        public override string SaveFile(string fileData, string directory)
        {
            return fileSaver.SaveFile(fileData, ".png", directory);
        }
    }
}
