using MediatRGen.Exceptions;
using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
{
    public class FileHelpers
    {
        public static void Create(string path, string fileName, string content)
        {
            string _path = PathHelper.GetPath(path, fileName);
            File.WriteAllText(_path, content);
        }
        public static void Create(string path, string fileName, object content)
        {

            string _path = PathHelper.GetPath(path, fileName);
           
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };

            var jsonString = JsonSerializer.Serialize(content, options);

            File.WriteAllText(_path, jsonString);
        }

        public static string Read() { return ""; }

        public static bool Delete() { return true; }

        public static bool Update() { return true; }

        public static bool CheckFile(string path, string fileName)
        {
            string _combinedPathWithFile = PathHelper.GetPath(path, fileName);

            if (File.Exists(_combinedPathWithFile))
                return true;

            return false;
        }

        public static string Get(string path)
        {

            return File.ReadAllText(path, Encoding.UTF8);
        }

        public static Config GetConfig()
        {

            string _file = Get(PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), GlobalState.Instance.ConfigFileName));

            if (string.IsNullOrEmpty(_file) == true)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }
            return JsonSerializer.Deserialize<Config>(_file);
        }

        public static void CreateConfig(Config config)
        {
            ConfigUpdateOrCreate(config);
            Console.WriteLine(LangHandler.Definitions().ConfigurationCreated);
        }

        private static void ConfigUpdateOrCreate(Config config)
        {
            string _configPath = PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), GlobalState.Instance.ConfigFileName);
            string _file = Get(_configPath);

            if (string.IsNullOrEmpty(_file) == true)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            File.Delete(_configPath);

            Create(DirectoryHelpers.GetCurrentDirectory(), GlobalState.Instance.ConfigFileName, config);

            Console.WriteLine(JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void UpdateConfig(Config config)
        {
            ConfigUpdateOrCreate(config);
        }


    }
}
