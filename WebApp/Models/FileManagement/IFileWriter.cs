using System;
using System.IO;

namespace WebApp.Models.FileManagement
{
    public interface IFileWriter
    {
        void SaveFile(string fileName, string fileData);
    }
}
