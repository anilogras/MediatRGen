using MediatRGen.Exceptions;
using MediatRGen.Languages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
{
    public class DirectoryHelpers
    {

        public static void CreateIsNotExist(string path, string folderName)
        {
            string _combinedPath = Path.Combine(path, folderName);

            if (!Directory.Exists(_combinedPath))
            {
                Directory.CreateDirectory(_combinedPath);
            }
        }
        public static bool Delete() { return true; }

        public static string GetAppDirectory()
        {
            return Environment.CurrentDirectory;
        }
    }
}
