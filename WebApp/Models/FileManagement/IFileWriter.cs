using System;
using System.IO;

namespace WebApp.Models.FileManagement
{
    public interface IFileWriter
    {
        void SaveImage(string fileName, string fileData);
    }
}
