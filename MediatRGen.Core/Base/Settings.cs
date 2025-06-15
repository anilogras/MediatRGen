using MediatRGen.Core.Concrete;
using MediatRGen.Core.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatRGen.Core.Base
{
    public class Settings : ISettings
    {

        [JsonIgnore]
        private readonly IDirectoryServices _directoryServices;

        [JsonIgnore]
        private readonly IFileService _fileService;

        public Settings()
        {
            
        }


        public Settings(IDirectoryServices directoryServices, IFileService fileService)
        {
            _directoryServices = directoryServices;
            _fileService = fileService;

            Lang = "tr";
            //Commands = ["create-solution", "create-repository", "create-config", "create-module"];
            ProjectName = "DenemeSolution";
            Modules = new List<ProjectModule>();
        }

        public string ProjectName { get; set; }
        public string Lang { get; set; }
        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
        public string ConfigFileName => "mediatr-config.json";

        public Settings Get()
        {
            string _configPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
            bool _fileExist = _fileService.CheckFile(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;

            if (_fileExist == false)
                return this;
            else
            {
                string _file = _fileService.Get(_configPath).Value;

                var options = new JsonSerializerOptions
                {
                    IncludeFields = true
                };

                return System.Text.Json.JsonSerializer.Deserialize<Settings>(_file , options);
            }
        }


        //todo updateteyi kontrol et , büyük ihtimalle güncellemeyecek

        public Settings Update()
        {
            string _configPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
            string _file = _fileService.Get(_configPath).Value;
            return System.Text.Json.JsonSerializer.Deserialize<Settings>(_file);
        }
    }
}
