using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WebApp.Models
{
    public interface IFileIdProvider
    {
        string GetId(string directory,string extention);
    }

    public class FileIdProvider : IFileIdProvider
    {
        public string GetId(string directory, string extention)
        {
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException();

            var files = Directory.GetFiles(directory).ToList();

            var actId = "0";

            files = (from file in files
                     where Path.GetExtension(file) == extention
                     select file).OrderBy((file) => file).ToList();

            foreach (var file in files)
            {
                if (Path.GetFileNameWithoutExtension(file) == actId)
                {
                    actId = GetNextId(actId);
                }
                else
                    break;
            }

            return actId;
        }

        private string GetNextId(string actId)
        {
            int c;
            string ret = "";

            for (int i = 0; i < actId.Length; i++)
            {
                c = actId[i];

                while (!Char.IsLetterOrDigit((char)(c+1)))
                    c++;           

                if (++c>90)
                {
                    if(i==actId.Length-1)
                    {
                        ret = actId.Remove(i, 1);
                        ret = ret.Insert(i, "00");
                        break;
                    }
                }
                else
                {
                    ret = actId.Remove(i);
                    ret = ret.Insert(i,((char)c).ToString());
                    break;
                }
            }

            return ret;
        }
    }
}
