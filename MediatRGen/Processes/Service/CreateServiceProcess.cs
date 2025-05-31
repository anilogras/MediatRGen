using Humanizer;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Cli.States;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Services;
using MediatRGen.Services.HelperServices;
using System.IO;

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

            _paths.ApplicationDirectory = $"{DirectoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Application\\Features\\{_paths.EntityLocalDirectory}\\{_paths.EntityPluralName}";
            _paths.EntityNameNotExt = _paths.EntityName.Substring(0, _paths.EntityName.IndexOf("."));
            _paths.EntityPluralName = _paths.EntityNameNotExt.Pluralize();
        }

        private void CreateBusinessRules()
        {
            string _applicationRulesDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Rules").Value;
            DirectoryServices.CreateIsNotExist(_applicationRulesDirectoryPath);
            string _businessRulesClassName = $"{_parameter.EntityName}BusinessRules";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_businessRulesClassName} -o {_applicationRulesDirectoryPath}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_applicationRulesDirectoryPath, _businessRulesClassName).Value, _applicationRulesDirectoryPath);
            ClassService.SetBaseInheritance(DirectoryServices.GetPath(_applicationRulesDirectoryPath, _businessRulesClassName).Value, "DenemeBaseModel");
        }

        private void CreateConstants()
        {
            string _applicationConstantsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Constants").Value;
            DirectoryServices.CreateIsNotExist(_applicationConstantsDirectoryPath);
            string _constantsClassName = $"{_parameter.EntityName}Messages";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_constantsClassName} -o {_applicationConstantsDirectoryPath}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_applicationConstantsDirectoryPath, _constantsClassName).Value, _applicationConstantsDirectoryPath);
        }

        private void CreateMapping()
        {
            string _applicationMappingProfilesDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory,  "Profiles").Value;
            DirectoryServices.CreateIsNotExist(_applicationMappingProfilesDirectoryPath);
            string _mappingProfilesClassName = $"{_parameter.EntityName}MappingProfiles";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_mappingProfilesClassName} -o {_applicationMappingProfilesDirectoryPath}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_applicationMappingProfilesDirectoryPath, _mappingProfilesClassName).Value, _applicationMappingProfilesDirectoryPath);
        }
    }
}
