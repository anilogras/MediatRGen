using MediatRGen.Core;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.HelperServices
{
    public static class DirectoryServices
    {
        public static ServiceResult<string> GetPath(params string[] paths)
        {
            string _text = "";

            try
            {
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
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>(string.Empty, false, LangHandler.Definitions().DirectoryPathError
                        + $" {string.Join("\\", paths)} ", new DirectoryException(ex.Message));
            }
            return new ServiceResult<string>(_text, true, LangHandler.Definitions().DirectoryPathCreated + $" {_text}");
        }

        public static ServiceResult<string> GetCurrentDirectory()
        {
            return new ServiceResult<string>(".\\DENSOL\\", true, "");
        }

        public static ServiceResult<bool> CreateIsNotExist(string path)
        {
            string _combinedPath = Path.Combine(path);

            try
            {
                if (!Directory.Exists(_combinedPath))
                {
                    Directory.CreateDirectory(_combinedPath);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().DirectoryCreateError + $" {path} ", new DirectoryException(ex.Message));
            }

            return new ServiceResult<bool>(true, true, LangHandler.Definitions().DirectoryCreated + $" {_combinedPath}");
        }

        public static ServiceResult<bool> Delete(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().DirectoryDeleteError + $" {path} ", new DirectoryException(ex.Message));
            }

            return new ServiceResult<bool>(true, true, LangHandler.Definitions().DirectoryDeleted + $" {path}");
        }

        public static void CreateIsNotExist(string path, string folderName)
        {
            string _combinedPath = Path.Combine(path, folderName);

            if (!Directory.Exists(_combinedPath))
            {
                Directory.CreateDirectory(_combinedPath);
                Console.WriteLine(LangHandler.Definitions().FolderCreated + $" {_combinedPath}");
            }

        }
    }
}
