using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Helpers
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

        public static void CreateIsNotExist(string path)
        {
            string _combinedPath = Path.Combine(path);

            if (!Directory.Exists(_combinedPath))
            {
                Directory.CreateDirectory(_combinedPath);
                Console.WriteLine(LangHandler.Definitions().DirectoryCreated + $" {_combinedPath}");
            }

        }
        public static void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public static string GetPath(params string[] paths)
        {
            string _text = "";
            int index = 1;

            foreach (var item in paths)
            {
                string _tempItem = "";

                if (item.EndsWith("\\"))
                    _tempItem = item.Substring(0, item.Length - 1);
                else
                    _tempItem = item;

                    _text += _tempItem;
                if (index != paths.Length)
                    _text += "\\";

                index++;
            }

            return _text;
        }

        public static string GetCurrentDirectory()
        {
            return ".\\DENSOL\\";
        }
    }
}
