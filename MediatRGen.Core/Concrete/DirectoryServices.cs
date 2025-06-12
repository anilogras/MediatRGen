using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class DirectoryServices : IDirectoryServices
    {
        public ServiceResult<string> GetPath(params string[] paths)
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
                return new ServiceResult<string>(string.Empty, false, LangHandler.Definitions().DirectoryPathError,
                    new DirectoryException(ex.Message));
            }

            return new ServiceResult<string>(_text, true, "");
        }
        public ServiceResult<string> ClearTwiceBackSlash(string path)
        {

            try
            {
                return new ServiceResult<string>(path.Replace("\\\\", "\\"), true, "");
            }
            catch (Exception ex)
            {

                return new ServiceResult<string>("", false, "", new DirectoryException(LangHandler.Definitions().BackSlashClearError));
            }

        }
        public ServiceResult<string> GetCurrentDirectory()
        {
            return new ServiceResult<string>(".\\DENSOL\\", true, "");
        }
        public ServiceResult<bool> CreateIsNotExist(string path)
        {
            string _combinedPath = Path.Combine(path);

            try
            {
                if (!Directory.Exists(_combinedPath))
                {
                    Directory.CreateDirectory(_combinedPath);
                    return new ServiceResult<bool>(true, true, LangHandler.Definitions().DirectoryCreated + $" {_combinedPath}");

                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().DirectoryCreateError + $" {path} ", new DirectoryException(ex.Message));
            }

            return new ServiceResult<bool>(true, true, "");

        }
        public ServiceResult<bool> Delete(string path)
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
    }
}
