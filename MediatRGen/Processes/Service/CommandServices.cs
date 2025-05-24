using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Services.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Service
{
    internal class CommandServices
    {

        private readonly ServiceCreateParameter _parameter;
        public CommandServices(ServiceCreateParameter parameter)
        {
            _parameter = parameter;
        }

        public void CreateCommands(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {
            string _applicationCommandsDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Commands";
            DirectoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);

            CreateCommand(_entityPath, _applicationModulePath, _pluralEntityName, _applicationCommandsDirectoryPath);
            DeleteCommand(_entityPath, _applicationModulePath, _pluralEntityName, _applicationCommandsDirectoryPath);
            UpdateCommand(_entityPath, _applicationModulePath, _pluralEntityName, _applicationCommandsDirectoryPath);


            //string _businessRulesClassName = $"{_parameter.EntityName}BusinessRules";
            //SystemProcessService.InvokeCommand($"dotnet new class -n {_businessRulesClassName} -o {_applicationCommandsDirectoryPath}");

            //ClassService.ChangeNameSpace(DirectoryServices.GetPath(_applicationCommandsDirectoryPath, _businessRulesClassName).Value, _applicationCommandsDirectoryPath);



            //ClassService.SetBaseInheritance(DirectoryServices.GetPath(_applicationCommandsDirectoryPath, _businessRulesClassName).Value, "DenemeBaseModel");
        }



        private void CreateCommand(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseCommandClasses(fullPath, "Create");
        }

        private void DeleteCommand(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseCommandClasses(fullPath, "Delete");
        }

        private void UpdateCommand(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseCommandClasses(fullPath, "Update");
        }

        private void CreateBaseCommandClasses(string fullPath, string workType)
        {
            string _commandPath = fullPath + $"\\{workType}";
            DirectoryServices.CreateIsNotExist(_commandPath);

            CommandConfiguration(workType, _commandPath);
            CommandHandlerConfiguration(workType, _commandPath);
        }

        private void CommandConfiguration(string workType, string _commandPath)
        {
            string _commandClassName = $"{workType}{_parameter.EntityName}Command";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandClassName} -o {_commandPath}");
            string _commandClassRoot = DirectoryServices.GetPath(_commandPath, _commandClassName).Value;
            ClassService.SetBaseInheritance(_commandClassRoot, $"Base{workType}Command<{_parameter.EntityName}>");
            ClassService.AddUsing(_commandClassRoot, $"Core.Application.MediatRBase.Commads.{workType}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandClassName).Value, _commandPath);
        }

        private void CommandHandlerConfiguration(string workType, string _commandPath)
        {
            string _commandHandlerClassName = $"{workType}{_parameter.EntityName}CommandHandler";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandHandlerClassName} -o {_commandPath}");
            string _commandHandlerClassRoot = DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value;
            ClassService.SetBaseInheritance(_commandHandlerClassRoot, $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Result, {_parameter.EntityName}>");
            ClassService.AddUsing(_commandHandlerClassRoot, $"Core.Application.MediatRBase.Commads.{workType}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value, _commandPath);
        }
    }
}
