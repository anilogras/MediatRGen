using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Services.Base;
using System.Text;
using System.Text.Json;

namespace MediatRGen.Services.HelperServices
{
    public class FileService
    {
        public static ServiceResult<bool> Create(string path, string fileName, string content)
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
        public static ServiceResult<bool> Create(string path, string fileName, object content)
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

        public static ServiceResult<bool> Create(string path, string fileName)
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

        public static ServiceResult<string?> Get(string path)
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

        public static ServiceResult<bool> UpdateConfig(string configFileName , object stateInstance)
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

        public static ServiceResult<string> FindFileRecursive(string directory, string targetFile)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (Path.GetFileName(file).Equals(targetFile, StringComparison.OrdinalIgnoreCase))
                        return new ServiceResult<string>(file, true, "");
                }

                foreach (var dir in Directory.GetDirectories(directory))
                {
                    string found = FindFileRecursive(dir, targetFile).Value;
                    if (found != null)
                        return new ServiceResult<string>(found, true, "");
                }

                return new ServiceResult<string>(string.Empty, false, LangHandler.Definitions().FileNotFound, new FileException(LangHandler.Definitions().FileNotFound));
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>(string.Empty, false, LangHandler.Definitions().FileNotFound, new FileException(ex.Message));
            }
        }

        public static ServiceResult<bool> CheckFile(string path, string fileName)
        {
            try
            {
                string _combinedPathWithFile = DirectoryServices.GetPath(path, fileName).Value;

                if (File.Exists(_combinedPathWithFile))
                    return new ServiceResult<bool>(true, true, LangHandler.Definitions().FileFounded);

                return new ServiceResult<bool>(false, true, LangHandler.Definitions().FileNotFound);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().FileFindError, new FileException(ex.Message));
            }
        }
    }
}
