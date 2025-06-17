using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatRGen.Core.Base
{
    public class Settings : ISettings
    {
        private readonly IDirectoryServices _directoryServices;
        private readonly IFileService _fileService;
        private ProgramConfigurations ProgramConfigurations { get; set; }

        [JsonConstructor]
        public Settings()
        {
            ProgramConfigurations = new ProgramConfigurations();
        }

        public Settings(IDirectoryServices directoryServices, IFileService fileService)
        {
            _directoryServices = directoryServices;
            _fileService = fileService;
            ProgramConfigurations = new ProgramConfigurations();
            this.Get();
        }

        public string ProjectName { get { return ProgramConfigurations.ProjectName; } set { ProgramConfigurations.ProjectName = value; } }
        public string Lang { get { return ProgramConfigurations.Lang; } set { ProgramConfigurations.Lang = value; } }
        public string SolutionName { get { return ProgramConfigurations.SolutionName; } set { ProgramConfigurations.SolutionName = value; } }
        public bool? Modul { get { return ProgramConfigurations.Modul; } set { ProgramConfigurations.Modul = value; } }
        public string Version { get { return ProgramConfigurations.Version; } set { ProgramConfigurations.Version = value; } }
        public bool UseModule { get { return ProgramConfigurations.UseModule; } set { ProgramConfigurations.UseModule = value; } }
        public bool UseGateway { get { return ProgramConfigurations.UseGateway; } set { ProgramConfigurations.UseGateway = value; } }
        public List<ProjectModule> Modules { get { return ProgramConfigurations.Modules; } set { ProgramConfigurations.Modules = value; } }
        public string ConfigFileName => "mediatr-config.json";

        public void Get()
        {
            string _configPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;
            bool _fileExist = _fileService.CheckFile(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value;

            if (_fileExist == false)
                ProgramConfigurations = new ProgramConfigurations();
            else
            {
                string _file = _fileService.Get(_configPath).Value;
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true
                };
                ProgramConfigurations = JsonSerializer.Deserialize<ProgramConfigurations>(_file, options);

                if (ProgramConfigurations == null)
                {

                }

            }
        }


        //todo updateteyi kontrol et , büyük ihtimalle güncellemeyecek

        public Settings Update()
        {
            ProgramConfigurations configuration = new ProgramConfigurations
            {
                Lang = this.Lang,
                ProjectName = this.ProjectName,
                Modul = this.Modul,
                Modules = this.Modules,
                SolutionName = this.SolutionName,
                UseGateway = this.UseGateway,
                UseModule = this.UseModule,
                Version = this.Version
            };

            _fileService.Create(_directoryServices.GetCurrentDirectory().Value, ConfigFileName, configuration , true);
            return JsonSerializer.Deserialize<Settings>(_fileService.Get(_directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, ConfigFileName).Value).Value);
        }
    }
}
