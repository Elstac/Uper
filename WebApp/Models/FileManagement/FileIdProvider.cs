using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WebApp.Models.FileManagement
{
    /// <summary>
    /// Provides generation of unique file ids
    /// </summary>
    public interface IFileIdProvider
    {
        /// <summary>
        /// Provides unique id for file with given extantion in directory
        /// </summary>
        /// <param name="directory">Path to directory</param>
        /// <param name="extention">File extention to look for</param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate new id string
        /// </summary>
        /// <param name="actId"></param>
        /// <returns></returns>
        private string GetNextId(string actId)
        {
            int c;
            string ret = "";

            for (int i = 0; i < actId.Length; i++)
            {
                c = actId[i];

                //bypas all special characters
                while (!Char.IsLetterOrDigit((char)(c+1)))
                    c++;           

                if (++c>90)
                {
                    //all unique names of lenght 2 taken. Adds new character to id
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
