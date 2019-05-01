using System.IO;

namespace WebApp.Models.FileManagement
{
    public class TextFileWriter : IFileWriter
    {
        public void SaveFile(string fileName, string fileData)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(fileData);
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}
