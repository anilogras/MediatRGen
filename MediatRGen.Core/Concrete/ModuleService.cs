using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete
{
    public class ModuleService : IModuleService
    {

        private readonly IParameterService _parameterService;
        private readonly IDirectoryServices _directoryServices;
        private readonly ISystemProcessService _systemProcessService;
        private readonly IWebApiService _webApiService;
        private readonly IClassLibraryService _classLibraryService;
        private readonly ISettings _settings;
        private readonly IFileService _fileService;
        private readonly IOutputService _outputService;

        public ModuleService(
            IParameterService parameterService,
            IDirectoryServices directoryServices,
            ISystemProcessService systemProcessService,
            IWebApiService webApiService,
            IClassLibraryService classLibraryService,
            ISettings settings,
            IOutputService outputService ,
            IFileService fileService
            )
        {
            _parameterService = parameterService;
            _directoryServices = directoryServices;
            _systemProcessService = systemProcessService;
            _webApiService = webApiService;
            _classLibraryService = classLibraryService;
            _settings = settings;
            _outputService = outputService;
            _fileService = fileService;
        }

        public void Create(CreateModuleBaseSchema settings)
        {
            //_parameterService.GetParameter<CreateModuleSchema>(command, ref settings);
            _parameterService.GetParameterFromConsole(settings, "ModuleName", LangHandler.Definitions().EnterModuleName);

            CheckModulNameIsExist(settings);
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
            _outputService.Info(settings.ModuleName + LangHandler.Definitions().ModuleCreated);
        }

        private void CheckModulNameIsExist(CreateModuleBaseSchema settings)
        {
            if (_settings.Modules.Where(x => x.Name == settings.ModuleName).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }
    }
}
