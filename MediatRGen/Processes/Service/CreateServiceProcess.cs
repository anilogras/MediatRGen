using Humanizer;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Module;
using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string _modulePath = $"{DirectoryHelpers.GetCurrentDirectory()}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Domain\\";
            string _entityPath = FileHelpers.FindFileRecursive(_modulePath, _parameter.EntityName + ".cs")?.Replace(_modulePath, "");

            if (string.IsNullOrEmpty(_entityPath))
                throw new FileException($"{LangHandler.Definitions().EntityNotFound} ({_parameter.ModuleName} -> {_parameter.EntityName})");

            _entityPath = _entityPath.Replace(_parameter.EntityName + ".cs", "");
            string _applicationModulePath = $"{DirectoryHelpers.GetCurrentDirectory()}src\\{_parameter.ModuleName}\\{GlobalState.Instance.SolutionName}.{_parameter.ModuleName}.Application\\Features\\";
            string _pluralEntityName = _parameter.EntityName.Pluralize();

            DirectoryHelpers.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName);

            CreateBusinessRules(_entityPath, _applicationModulePath, _pluralEntityName);
            CreateConstants(_entityPath, _applicationModulePath, _pluralEntityName);
            CreateMapping(_entityPath, _applicationModulePath, _pluralEntityName);


            DirectoryHelpers.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName + "\\Commands");
            DirectoryHelpers.CreateIsNotExist(_applicationModulePath + _entityPath + _pluralEntityName + "\\Queries");

            Console.WriteLine(LangHandler.Definitions().ServiceCreated);
        }

        private void CreateBusinessRules(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationRulesDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Rules";
            DirectoryHelpers.CreateIsNotExist(_applicationRulesDirectoryPath);
            string _businessRulesClassName = $"{_parameter.EntityName}BusinessRules";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_businessRulesClassName} -o {_applicationRulesDirectoryPath}");

            ClassHelper.ChangeNameSpace(DirectoryHelpers.GetPath(_applicationRulesDirectoryPath, _businessRulesClassName), _applicationRulesDirectoryPath);

        }

        private void CreateConstants(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationConstantsDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Constants";
            DirectoryHelpers.CreateIsNotExist(_applicationConstantsDirectoryPath);
            string _constantsClassName = $"{_parameter.EntityName}Messages";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_constantsClassName} -o {_applicationConstantsDirectoryPath}");
            ClassHelper.ChangeNameSpace(DirectoryHelpers.GetPath(_applicationConstantsDirectoryPath, _constantsClassName), _applicationConstantsDirectoryPath);

        }

        private void CreateMapping(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationMappingProfilesDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Profiles";
            DirectoryHelpers.CreateIsNotExist(_applicationMappingProfilesDirectoryPath);
            string _mappingProfilesClassName = $"{_parameter.EntityName}MappingProfiles";
            SystemProcessHelpers.InvokeCommand($"dotnet new class -n {_mappingProfilesClassName} -o {_applicationMappingProfilesDirectoryPath}");
            ClassHelper.ChangeNameSpace(DirectoryHelpers.GetPath(_applicationMappingProfilesDirectoryPath, _mappingProfilesClassName), _applicationMappingProfilesDirectoryPath);

        }
    }
}
