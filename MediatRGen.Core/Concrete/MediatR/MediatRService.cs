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

            _paths = new ServicePaths();
            _parameter = settings;
            CreatePaths();
            _directoryServices.CreateIsNotExist(_paths.ApplicationDirectory + "\\" + _paths.EntityNameNotExt);

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

        private void CreatePaths()
        {
            _paths.DomainPath = $"{_directoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{_settings.SolutionName}.{_parameter.ModuleName}.Domain\\";
            _paths.EntityPath = _fileService.FindFileRecursive(_paths.DomainPath, _parameter.EntityName + ".cs").Value;
            _paths.EntityLocalDirectory = _paths.EntityPath.Replace(_paths.DomainPath, "");
            _paths.EntityLocalDirectory = _paths.EntityLocalDirectory.Substring(0, _paths.EntityLocalDirectory.LastIndexOf("\\"));
            _paths.EntityName = _paths.EntityPath.Substring(_paths.EntityPath.LastIndexOf("\\") + 1);
            _paths.EntityDirectory = _paths.EntityPath.Substring(0, _paths.EntityPath.LastIndexOf("\\"));
            _paths.EntityPathNotExt = _paths.EntityPath.Substring(0, _paths.EntityPath.LastIndexOf("."));
            if (string.IsNullOrEmpty(_paths.EntityPath))
                throw new FileException($"{LangHandler.Definitions().EntityNotFound} ({_parameter.ModuleName} -> {_parameter.EntityName})");
            _paths.EntityNameNotExt = _paths.EntityName.Substring(0, _paths.EntityName.IndexOf("."));
            _paths.EntityPluralName = PluralizationProvider.Pluralize(_paths.EntityNameNotExt);
            _paths.ApplicationDirectory = $"{_directoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{_settings.SolutionName}.{_parameter.ModuleName}.Application\\Features\\{_paths.EntityLocalDirectory}\\{_paths.EntityPluralName}CQRS";
            _paths.ControllerDirectory = $"{_directoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{_settings.SolutionName}.{_parameter.ModuleName}.API\\Controllers\\{_paths.EntityLocalDirectory}";
            _paths.ControllerPath = $"{_directoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{_settings.SolutionName}.{_parameter.ModuleName}.API\\Controllers\\{_paths.EntityLocalDirectory}\\{_paths.EntityNameNotExt}Controller";
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
                _paths.EntityDirectory ,
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
                @$"CreateMap<{_paths.EntityNameNotExt}, Create{_paths.EntityNameNotExt}Command>().ReverseMap();",
                @$"CreateMap<{_paths.EntityNameNotExt}, Create{_paths.EntityNameNotExt}Response>().ReverseMap();",
                @$"CreateMap<{_paths.EntityNameNotExt}, GetList{_paths.EntityNameNotExt}ListItemDto>().ReverseMap();",
                @$"CreateMap<Paging<{_paths.EntityNameNotExt}>, GetListResponse<GetList{_paths.EntityNameNotExt}ListItemDto>>().ReverseMap();",
                @$"CreateMap<GetById{_paths.EntityNameNotExt}Response, {_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Update{_paths.EntityNameNotExt}Command, {_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Update{_paths.EntityNameNotExt}Response, {_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityNameNotExt}Response  ,{_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityNameNotExt}Command, {_paths.EntityNameNotExt}>().ReverseMap();"
            };

            _classConfigs.Add(_classConfig);
        }
    }
}
