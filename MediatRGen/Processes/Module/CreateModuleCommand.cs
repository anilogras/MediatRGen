using MediatRGen.Cli.Models;
using MediatRGen.Cli.States;
using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Module
{
    public class CreateModuleCommand : Command<CreateModuleSchema>
    {

        private readonly IParameterService _parameterService;
        private readonly IDirectoryServices _directoryServices;
        private readonly ISystemProcessService _systemProcessService;
        private readonly IWebApiService _webApiService;
        private readonly IClassLibraryService _classLibraryService;
        private readonly ISettings _settings;
        private readonly IFileService _fileService;

        public CreateModuleCommand(
            IParameterService parameterService,
            IDirectoryServices directoryServices,
            ISystemProcessService systemProcessService,
            IWebApiService webApiService,
            IClassLibraryService classLibraryService,
            ISettings settings)
        {
            _parameterService = parameterService;
            _directoryServices = directoryServices;
            _systemProcessService = systemProcessService;
            _webApiService = webApiService;
            _classLibraryService = classLibraryService;
            _settings = settings;
        }

        public override int Execute(CommandContext context, CreateModuleSchema settings)
        {
            _parameterService.GetParameter<CreateModuleSchema>(command, ref settings);
            _parameterService.GetParameterFromConsole(settings, "ModuleName", LangHandler.Definitions().EnterModuleName);


            CheckModulNameIsExist();
            _directoryServices.CreateIsNotExist(_directoryServices.GetCurrentDirectory().Value + "src\\" + settings.ModuleName);

            _classLibraryService.Create(settings.ModuleName + "." + "Domain", _directoryServices.GetPath(settings.ModuleName).Value, _settings.ProjectName, _settings.SolutionName);
            _classLibraryService.Create(settings.ModuleName + "." + "Application", _directoryServices.GetPath(settings.ModuleName).Value, _settings.ProjectName, _settings.SolutionName);
            _classLibraryService.Create(settings.ModuleName + "." + "Infrastructure", _directoryServices.GetPath(settings.ModuleName).Value, _settings.ProjectName, _settings.SolutionName);
            _webApiService.Create(settings.ModuleName + "." + "API", _directoryServices.GetPath(settings.ModuleName).Value, _settings.ProjectName, _settings.SolutionName);

            _systemProcessService.BuildProject(_settings.ProjectName);

            _settings.Modules.Add(new ProjectModule()
            {
                Name = settings.ModuleName
            });

            _fileService.UpdateConfig(_settings.ConfigFileName, _settings);

            Console.WriteLine(settings.ModuleName + LangHandler.Definitions().ModuleCreated);

            return 0;
        }

        private void CheckModulNameIsExist()
        {
            if (_settings.Modules.Where(x => x.Name == settings.ModuleName).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }
    }
}
