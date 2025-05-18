using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Helpers
{
    public class DirectoryHelpers
    {

        public static void CreateIsNotExist(string path, string folderName)
        {
            string _combinedPath = Path.Combine(path, folderName);

            if (!Directory.Exists(_combinedPath))
            {
                Directory.CreateDirectory(_combinedPath);
                Console.WriteLine(LangHandler.Definitions().FolderCreated + $" {_combinedPath}");
            }

        }
        public static void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path , true);
            }
        }

        public static string GetPath(params string[] paths)
        {
            return string.Join('\\', paths).Replace("\\", "/").Replace("//", "/");
        }

        public static string GetCurrentDirectory()
        {
            return "./DENSOL/";
        }
    }
}
