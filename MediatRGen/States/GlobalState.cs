using MediatRGen.Core.Concrete;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using System.Text.Json.Serialization;

namespace MediatRGen.Cli.States
{
    public class GlobalState
    {

        private readonly IDirectoryServices _directoryServices;

        public GlobalState(IDirectoryServices directoryServices)
        {
            _directoryServices = directoryServices;

            Lang = "tr";
            Commands = ["create-solution", "create-repository", "create-config", "create-module"];
            ProjectName = "DenemeSolution";
            Modules = new List<ProjectModule>();
        }
        public static string ConfigFileName = "mediatr-config.json";

        private static GlobalState _instance;

        public  GlobalState Instance
        {
            get
            {

                if (_instance == null)
                {
                    string _configPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
                    bool _fileExist = FileService.CheckFile(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;

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
