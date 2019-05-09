using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.FileManagement
{
    public interface IFileReader<T>
    {
        T ReadFileContent(string path);
    }
    public class TextFileReader : IFileReader<string>
    {
        public string ReadFileContent(string path)
        {
            string ret = "";
            using (var sr = new StreamReader(path))
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }
    }
}
