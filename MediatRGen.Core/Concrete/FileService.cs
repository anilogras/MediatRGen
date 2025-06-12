using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System.Text;
using System.Text.Json;

namespace MediatRGen.Core.Concrete
{
    public class FileService : IFileService
    {
        public ServiceResult<bool> Create(string path, string fileName, string content)
        {
            try
            {
                string _path = DirectoryServices.GetPath(path, fileName).Value;

                if (!File.Exists(_path))
                {
                    File.WriteAllText(_path, content);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().FileCreateError, new FileException(ex.Message));
            }

            return new ServiceResult<bool>(true, true, LangHandler.Definitions().FileCreated + $" ({fileName})", null);

        }
        public ServiceResult<bool> Create(string path, string fileName, object content)
        {
            try
            {
                string _path = DirectoryServices.GetPath(path, fileName).Value;
                if (!File.Exists(_path))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    var jsonString = JsonSerializer.Serialize(content, options);

                    File.WriteAllText(_path, jsonString);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().FileCreateError, new FileException(ex.Message));
            }

            return new ServiceResult<bool>(true, true, LangHandler.Definitions().FileCreated + $" ({fileName})", null);
        }
        public ServiceResult<bool> Create(string path, string fileName)
        {
            try
            {
                string _path = DirectoryServices.GetPath(path, fileName).Value;

                if (!File.Exists(_path))
                {
                    File.Create(_path).Close();
                    Console.WriteLine(LangHandler.Definitions().FileCreated + $" ({fileName})");
                }

                return new ServiceResult<bool>(true, true, "", null);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().FileCreateError, new FileException(ex.Message));
            }
        }
        public ServiceResult<string?> Get(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string _file = File.ReadAllText(path, Encoding.UTF8);
                    return new ServiceResult<string?>(_file, true, "", null);
                }

                return new ServiceResult<string?>(null, false, LangHandler.Definitions().FileNotFound, new FileException(LangHandler.Definitions().FileNotFound));

            }
            catch (Exception ex)
            {
                return new ServiceResult<string?>(null, false, LangHandler.Definitions().FileReadError, new FileException(ex.Message));
            }
        }
        public ServiceResult<bool> UpdateConfig(string configFileName, object stateInstance)
        {

            try
            {
                string _configPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, configFileName).Value;

                string? _file = Get(_configPath).Value;

                if (string.IsNullOrEmpty(_file) == true)
                {
                    throw new FileException(LangHandler.Definitions().ConfigNotFound);
                }

                File.Delete(_configPath);

                Create(DirectoryServices.GetCurrentDirectory().Value, configFileName, stateInstance);

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().ConfigUpdated, null);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ConfigUpdateError, new FileException(ex.Message));
            }
        }
        public ServiceResult<string> FindFileRecursive(string directory, string targetFile)
        {
            try
            {
                string _searchResult = FindFile(directory, targetFile);

                if (_searchResult != null)
                    return new ServiceResult<string>(_searchResult, true, "");
                // return new ServiceResult<string>(_searchResult, true, LangHandler.Definitions().FileFounded);
                else
                    return new ServiceResult<string>(null, false, LangHandler.Definitions().FileNotFound, new FileException(LangHandler.Definitions().FileNotFound));

            }
            catch (Exception ex)
            {
                return new ServiceResult<string>(string.Empty, false, LangHandler.Definitions().FileNotFound, new FileException(ex.Message));
            }
        }
        private string FindFile(string directory, string targetFile)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file).Equals(targetFile, StringComparison.OrdinalIgnoreCase))
                    return file;
            }

            foreach (var dir in Directory.GetDirectories(directory))
            {
                string found = FindFile(dir, targetFile);
                if (found != null)
                    return found;
            }

            return null;
        }
        public ServiceResult<bool> CheckFile(string path, string fileName)
        {
            try
            {
                string _combinedPathWithFile = DirectoryServices.GetPath(path, fileName).Value;

                if (File.Exists(_combinedPathWithFile))
                    return new ServiceResult<bool>(true, true, "");
                //return new ServiceResult<bool>(true, true, LangHandler.Definitions().FileFounded);

                return new ServiceResult<bool>(false, true, LangHandler.Definitions().FileNotFound);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().FileFindError, new FileException(ex.Message));
            }
        }
    }
}
