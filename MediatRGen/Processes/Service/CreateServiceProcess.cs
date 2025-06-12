using Humanizer;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Cli.States;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediatRGen.Cli.Processes.Service
{
    public class CreateServiceProcess : BaseProcess
    {

        private ServiceCreateParameter _parameter;
        private readonly ServicePaths _paths;

        public CreateServiceProcess(string command)
        {
            ParameterService.GetParameter<ServiceCreateParameter>(command, ref _parameter);
            ParameterService.GetParameterFromConsole(_parameter, "EntityName", LangHandler.Definitions().EnterEntityName);
            ParameterService.GetParameterFromConsole(_parameter, "ModuleName", LangHandler.Definitions().EnterModuleName);

            _paths = new ServicePaths();

            Execute();

        }


        private void Execute()
        {
            CreatePaths();

            DirectoryServices.CreateIsNotExist(_paths.ApplicationDirectory + "\\" + _paths.EntityNameNotExt);

            CreateBusinessRules();
            CreateConstants();
            CreateMapping();


            CommandServices commandServices = new CommandServices(_parameter, _paths);
            commandServices.CreateCommands();


            QueryServices queryServices = new QueryServices(_parameter, _paths);
            queryServices.CreateQueries();


            DirectoryServices.CreateIsNotExist(_paths.ApplicationDirectory + "\\DTOs");
            Console.WriteLine(LangHandler.Definitions().ServiceCreated);
        }

        private void CreatePaths()
        {
            _paths.DomainPath = $"{DirectoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Domain\\";
            _paths.EntityPath = FileService.FindFileRecursive(_paths.DomainPath, _parameter.EntityName + ".cs").Value;
            _paths.EntityLocalDirectory = _paths.EntityPath.Replace(_paths.DomainPath, "");
            _paths.EntityLocalDirectory = _paths.EntityLocalDirectory.Substring(0, _paths.EntityLocalDirectory.LastIndexOf("\\"));
            _paths.EntityName = _paths.EntityPath.Substring(_paths.EntityPath.LastIndexOf("\\") + 1);
            _paths.EntityDirectory = _paths.EntityPath.Substring(0, _paths.EntityPath.LastIndexOf("\\"));
            _paths.EntityPathNotExt = _paths.EntityPath.Substring(0, _paths.EntityPath.LastIndexOf("."));
            if (string.IsNullOrEmpty(_paths.EntityPath))
                throw new FileException($"{LangHandler.Definitions().EntityNotFound} ({_parameter.ModuleName} -> {_parameter.EntityName})");
            _paths.EntityNameNotExt = _paths.EntityName.Substring(0, _paths.EntityName.IndexOf("."));
            _paths.EntityPluralName = _paths.EntityNameNotExt.Pluralize();
            _paths.ApplicationDirectory = $"{DirectoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Application\\Features\\{_paths.EntityLocalDirectory}\\{_paths.EntityPluralName}";

        }

        private void CreateBusinessRules()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Name = $"{_parameter.EntityName}BusinessRules";
            _classConfig.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Rules").Value;
            _classConfig.BaseInheritance = "DenemeBaseModel";

            ClassService.CreateClass(_classConfig);
        }

        private void CreateConstants()
        {

            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Name = $"{_parameter.EntityName}Messages";
            _classConfig.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Constants").Value;

            ClassService.CreateClass(_classConfig);

        }

        private void CreateMapping()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Mapping").Value;
            _classConfig.Name = $"{_parameter.EntityName}MappingProfiles";
            _classConfig.BaseInheritance = "Profile";
            _classConfig.Usings = new List<string> {
                "AutoMapper" ,
                _paths.EntityDirectory ,
                "Core.Persistence.Pagination" ,
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
                @$"CreateMap<Updated{_paths.EntityNameNotExt}Response, {_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityNameNotExt}Response  ,{_paths.EntityNameNotExt}>().ReverseMap();",
                @$"CreateMap<Delete{_paths.EntityNameNotExt}Command, {_paths.EntityNameNotExt}>().ReverseMap();"
            };

            ClassService.CreateClass(_classConfig);
        }
    }
}
