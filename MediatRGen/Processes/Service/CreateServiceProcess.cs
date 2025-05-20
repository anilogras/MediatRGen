using Humanizer;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Core;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Helpers;
using MediatRGen.Core.States;
using MediatRGen.Services.HelperServices;

namespace MediatRGen.Cli.Processes.Service
{
    public class CreateServiceProcess : BaseProcess
    {

        private ServiceCreateParameter _parameter;

        public CreateServiceProcess(string command)
        {
            ParameterHelper.GetParameter<ServiceCreateParameter>(command, ref _parameter);
            ParameterHelper.GetParameterFromConsole(_parameter, "EntityName", LangHandler.Definitions().EnterEntityName);
            ParameterHelper.GetParameterFromConsole(_parameter, "ModuleName", LangHandler.Definitions().EnterModuleName);

            Execute();
        }

        private void Execute()
        {
            string _modulePath = $"{DirectoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Domain\\";
            string _entityPath = FileHelpers.FindFileRecursive(_modulePath, _parameter.EntityName + ".cs")?.Replace(_modulePath, "");

            if (string.IsNullOrEmpty(_entityPath))
                throw new FileException($"{LangHandler.Definitions().EntityNotFound} ({_parameter.ModuleName} -> {_parameter.EntityName})");

            _entityPath = _entityPath.Replace(_parameter.EntityName + ".cs", "");
            string _applicationModulePath = $"{DirectoryServices.GetCurrentDirectory().Value}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Application\\Features\\";
            string _pluralEntityName = _parameter.EntityName.Pluralize();

            DirectoryServices.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName);

            CreateBusinessRules(_entityPath, _applicationModulePath, _pluralEntityName);
            CreateConstants(_entityPath, _applicationModulePath, _pluralEntityName);
            CreateMapping(_entityPath, _applicationModulePath, _pluralEntityName);


            DirectoryServices.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName + "\\Commands");
            DirectoryServices.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName + "\\Queries");

            Console.WriteLine(LangHandler.Definitions().ServiceCreated);
        }

        private void CreateBusinessRules(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationRulesDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Rules";
            DirectoryServices.CreateIsNotExist(_applicationRulesDirectoryPath);
            string _businessRulesClassName = $"{_parameter.EntityName}BusinessRules";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_businessRulesClassName} -o {_applicationRulesDirectoryPath}");

            ClassHelper.ChangeNameSpace(DirectoryServices.GetPath(_applicationRulesDirectoryPath, _businessRulesClassName).Value, _applicationRulesDirectoryPath);
            ClassHelper.SetBaseInheritance(DirectoryServices.GetPath(_applicationRulesDirectoryPath, _businessRulesClassName).Value , "DenemeBaseModel");
        }

        private void CreateConstants(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationConstantsDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Constants";
            DirectoryServices.CreateIsNotExist(_applicationConstantsDirectoryPath);
            string _constantsClassName = $"{_parameter.EntityName}Messages";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_constantsClassName} -o {_applicationConstantsDirectoryPath}");
            ClassHelper.ChangeNameSpace(DirectoryServices.GetPath(_applicationConstantsDirectoryPath, _constantsClassName).Value, _applicationConstantsDirectoryPath);
        }

        private void CreateMapping(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationMappingProfilesDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Profiles";
            DirectoryServices.CreateIsNotExist(_applicationMappingProfilesDirectoryPath);
            string _mappingProfilesClassName = $"{_parameter.EntityName}MappingProfiles";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_mappingProfilesClassName} -o {_applicationMappingProfilesDirectoryPath}");
            ClassHelper.ChangeNameSpace(DirectoryServices.GetPath(_applicationMappingProfilesDirectoryPath, _mappingProfilesClassName).Value, _applicationMappingProfilesDirectoryPath);

        }
    }
}
