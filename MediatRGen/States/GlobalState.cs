using MediatRGen.Core.Concrete;
using MediatRGen.Core.Models;
using System.Text.Json.Serialization;

namespace MediatRGen.Cli.States
{
    public class GlobalState
    {

        public GlobalState()
        {
            Lang = "tr";
            Commands = ["create-solution", "create-repository", "create-config", "create-module"];
            ProjectName = "DenemeSolution";
            Modules = new List<ProjectModule>();
        }
        public static string ConfigFileName = "mediatr-config.json";

        private static GlobalState _instance;

        public static GlobalState Instance
        {
            get
            {

                if (_instance == null)
                {
                    string _configPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
                    bool _fileExist = FileService.CheckFile(DirectoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;

                    if (_fileExist == false)
                        _instance = new GlobalState();
                    else
                    {
                        string _file = FileService.Get(_configPath).Value;
                        _instance = System.Text.Json.JsonSerializer.Deserialize<GlobalState>(_file);
                    }

                }
                return _instance;
            }
        }

        public static void UpdateInstance()
        {
            string _configPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
            string _file = FileService.Get(_configPath).Value;
            _instance = System.Text.Json.JsonSerializer.Deserialize<GlobalState>(_file);
        }

        public string ProjectName { get; set; }

        public string Lang { get; set; }

        [JsonIgnore]
        public string[] Commands { get; set; }

        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
