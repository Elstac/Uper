using System;

namespace WebApp.Models.ImageManagement
{
    /// <summary>
    /// Provides saving imageData form string to image file
    /// </summary>
    public interface IImageSaver
    {
        /// <summary>
        /// Save endoced image data string to new file
        /// </summary>
        /// <param name="imageData">string containing encoded image data</param>
        /// <param name="fileExtention">Extention of created file</param>
        /// <param name="directory">Path of directory where file will be saved</param>
        /// <returns></returns>
        string SaveImage(string imageData, string fileExtention, string directory);
    }
    public class ImageSaver : IImageSaver
    {
        private IFileIdProvider idProvider;
        private IImageWriter imageWriter;

        public ImageSaver(IFileIdProvider idProvider, IImageWriter imageWriter)
        {
            this.idProvider = idProvider;
            this.imageWriter = imageWriter;
        }

        public string SaveImage(string imageData, string fileExtention, string directory)
        {
            var id = idProvider.GetId(directory, fileExtention);

            imageWriter.SaveImage(directory+id + ".png", imageData);

            return id;
        }
    }
}
