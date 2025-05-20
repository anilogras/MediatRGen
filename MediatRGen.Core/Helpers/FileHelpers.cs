using MediatRGen.Core.Exceptions;
using MediatRGen.Core.States;
using System.Text;
using System.Text.Json;

namespace MediatRGen.Core.Helpers
{
    public class FileHelpers
    {
        public static void Create(string path, string fileName, string content)
        {
            string _path = DirectoryHelpers.GetPath(path, fileName);
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, content);
                Console.WriteLine(LangHandler.Definitions().FileCreated + $" ({fileName})");
            }
        }
        public static void Create(string path, string fileName, object content)
        {

            string _path = DirectoryHelpers.GetPath(path, fileName);
            if (!File.Exists(_path))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var jsonString = JsonSerializer.Serialize(content, options);

                File.WriteAllText(_path, jsonString);
                Console.WriteLine(LangHandler.Definitions().FileCreated + $" ({fileName})");
            }
        }

        public static void Create(string path, string fileName)
        {
            string _path = DirectoryHelpers.GetPath(path, fileName);

            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
                Console.WriteLine(LangHandler.Definitions().FileCreated + $" ({fileName})");
            }
        }

        public static string Read() { return ""; }
        public static bool Delete() { return true; }
        public static bool Update() { return true; }
        public static bool CheckFile(string path, string fileName)
        {
            string _combinedPathWithFile = DirectoryHelpers.GetPath(path, fileName);

            if (File.Exists(_combinedPathWithFile))
                return true;

            return false;
        }
        public static string? Get(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path, Encoding.UTF8);
            }

            return null;
        }
        public static void UpdateConfig()
        {

            string _configPath = DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), GlobalState.ConfigFileName);
            string _file = Get(_configPath);

            if (string.IsNullOrEmpty(_file) == true)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            File.Delete(_configPath);

            Create(DirectoryHelpers.GetCurrentDirectory(), GlobalState.ConfigFileName, GlobalState.Instance);
        }

        public static string FindFileRecursive(string directory, string targetFile)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file).Equals(targetFile, StringComparison.OrdinalIgnoreCase))
                    return file;
            }

            foreach (var dir in Directory.GetDirectories(directory))
            {
                string found = FindFileRecursive(dir, targetFile);
                if (found != null)
                    return found;
            }

            return null;
        }


    }
}
