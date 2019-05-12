using System;

namespace WebApp.Models.FileManagement
{
    /// <summary>
    /// Provides saving imageData form string to image file
    /// </summary>
    public interface IFileSaver
    {
        /// <summary>
        /// Save endoced image data string to new file
        /// </summary>
        /// <param name="fileData">string containing file data</param>
        /// <param name="fileExtention">Extention of created file</param>
        /// <param name="directory">Path of directory where file will be saved</param>
        /// <returns></returns>
        string SaveFile(string fileData, string fileExtention, string directory);
    }
    public class FileSaver : IFileSaver
    {
        private IFileIdProvider idProvider;
        private IFileWriter fileWriter;

        public FileSaver(IFileIdProvider idProvider, IFileWriter fileWriter)
        {
            this.idProvider = idProvider;
            this.fileWriter = fileWriter;
        }

        public string SaveFile(string fileData, string fileExtention, string directory)
        {
            var id = idProvider.GetId(directory, fileExtention);

            fileWriter.SaveFile(directory+id + fileExtention, fileData);

            return id;
        }
    }
}
