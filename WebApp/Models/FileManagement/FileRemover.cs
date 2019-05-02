using System;
using System.IO;

namespace WebApp.Models.FileManagement
{
    public interface IFileRemover
    {
        void RemoveImage(string fileId, string directory,string fileExtention);
    }
    public class FileRemover : IFileRemover
    {
        public void RemoveImage(string fileId, string directory, string fileExtention)
        {
            var name = directory + fileId + fileExtention;

            File.Delete(name);
        }
    }
}
