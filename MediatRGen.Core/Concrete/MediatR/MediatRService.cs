using MediatRGen.Cli.Processes.MediatR;
using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using PluralizeService.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete.MediatR
{
    internal class MediatRService : IMediatRService
    {
        private ServicePaths _paths;
        private CreateServiceBaseSchema _parameter;
        private IList<ClassConfiguration> _classConfigs;

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;
        private readonly IParameterService _parameterService;
        private readonly ISettings _settings;
        private readonly IFileService _fileService;

        public MediatRService(
            IDirectoryServices directoryServices,
            IClassService classService,
            INameSpaceService nameSpaceService,
            IParameterService parameterService,
            ISettings settings,
            IFileService fileService)
        {
            _directoryServices = directoryServices;
            _classService = classService;
            _nameSpaceService = nameSpaceService;
            _parameterService = parameterService;
            _settings = settings;
            _fileService = fileService;
            _classConfigs = new List<ClassConfiguration>();
        }

        public async void Create(CreateServiceBaseSchema settings)
        {
            //_parameterService.GetParameter<CreateServiceSchema>(command, ref settings);
            _parameterService.GetParameterFromConsole(settings, "EntityName", LangHandler.Definitions().EnterEntityName);
            _parameterService.GetParameterFromConsole(settings, "ModuleName", LangHandler.Definitions().EnterModuleName);

            _paths = new ServicePaths(settings.ModuleName, settings.EntityName);
            _parameter = settings;

            _directoryServices.CreateIsNotExist(_paths.ApplicationDirectory + "\\" + _paths.EntityName);

            CreateBusinessRules();
            CreateConstants();
            CreateMapping();

            CommandServices commandServices = new CommandServices(settings, _paths, _classConfigs, _directoryServices, _classService, _nameSpaceService);
            commandServices.CreateCommands();

            QueryServices queryServices = new QueryServices(settings, _paths, _classConfigs, _directoryServices, _classService, _nameSpaceService);
            queryServices.CreateQueries();

            ControllerService controllerService = new ControllerService(settings, _paths, _classConfigs, _directoryServices, _classService, _nameSpaceService);
            controllerService.CreateController();

            _directoryServices.CreateIsNotExist(_paths.ApplicationDirectory + "\\DTOs");

            await _classService.CreateClass(_classConfigs.ToList());

            Console.WriteLine(LangHandler.Definitions().ServiceCreated);
        }


        private void CreateBusinessRules()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Name = $"{_parameter.EntityName}BusinessRules";
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Rules").Value;

            _classConfigs.Add(_classConfig);
        }

        private void CreateConstants()
        {

            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Name = $"{_parameter.EntityName}Messages";
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Constants").Value;

            _classConfigs.Add(_classConfig);
        }

        private void CreateMapping()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Mapping").Value;
            _classConfig.Name = $"{_parameter.EntityName}MappingProfiles";
            _classConfig.BaseInheritance = "Profile";
            _classConfig.Usings = new List<string> {
                "AutoMapper" ,
                _paths.EntityFolder ,
                "Core.Persistence.Pagination" ,
                _paths.ApplicationDirectory+".Queries.GetById",
                _paths.ApplicationDirectory+".Queries.GetList",
                _paths.ApplicationDirectory+".Queries.GetListDynamic",
                _paths.ApplicationDirectory+".Queries.GetListPaged",
                _paths.ApplicationDirectory+".Commands.Create",
                _paths.ApplicationDirectory+".Commands.Delete",
                _paths.ApplicationDirectory+".Commands.Update",
            };

            _classConfig.Constructor = true;
            _classConfig.ConstructorCodes = new List<string>
            {
                @$"CreateMap<{_paths.EntityName}, Create{_paths.EntityName}Command>().ReverseMap();",
                @$"CreateMap<{_paths.EntityName}, Create{_paths.EntityName}Response>().ReverseMap();",
                @$"CreateMap<{_paths.EntityName}, GetList{_paths.EntityName}ListItemDto>().ReverseMap();",
                @$"CreateMap<Paging<{_paths.EntityName}>, GetListResponse<GetList{_paths.EntityName}ListItemDto>>().ReverseMap();",
                @$"CreateMap<GetById{_paths.EntityName}Response, {_paths.EntityName}>().ReverseMap();",
                @$"CreateMap<Update{_paths.EntityName}Command, {_paths.EntityName}>().ReverseMap();",
                @$"CreateMap<Update{_paths.EntityName}Response, {_paths.EntityName}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityName}Response  ,{_paths.EntityName}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityName}Command, {_paths.EntityName}>().ReverseMap();"
            };

            _classConfigs.Add(_classConfig);
        }
    }
}
