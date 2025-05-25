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
        private readonly ServicePaths _paths;

        public CommandServices(ServiceCreateParameter parameter, ServicePaths paths)
        {
            _parameter = parameter;
            _paths = paths;
        }


        public void CreateCommands()
        {
            string _applicationCommandsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationModulePath, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands").Value;
            DirectoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);

            CreateCommand();
            DeleteCommand();
            UpdateCommand();

        }



        private void CreateCommand()
        {
            CreateBaseCommandClasses("Create");
        }

        private void DeleteCommand()
        {
            CreateBaseCommandClasses("Delete");
        }

        private void UpdateCommand()
        {
            CreateBaseCommandClasses("Update");
        }

        private void CreateBaseCommandClasses(string workType)
        {
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationModulePath, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            DirectoryServices.CreateIsNotExist(_commandPath);

            CommandConfiguration(workType);
            CommandHandlerConfiguration(workType);
        }

        private void CommandConfiguration(string workType)
        {
            string _commandClassName = $"{workType}{_parameter.EntityName}Command";
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationModulePath, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandClassName} -o {_commandPath}");

            string _commandClassRoot = DirectoryServices.GetPath(_commandPath, _commandClassName).Value;
            ClassService.SetBaseInheritance(_commandClassRoot, $"Base{workType}Command<{_parameter.EntityName}>");
            ClassService.AddUsing(_commandClassRoot, $"Core.Application.BaseCQRS.Commands.{workType}");

            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandClassName).Value, _commandPath);

            string entityNameSpace = ClassService.GetNameSpace(_commandClassRoot).Value;
            ClassService.AddUsing(_commandClassRoot, entityNameSpace);

        }

        private void CommandHandlerConfiguration(string workType)
        {

            string _commandHandlerClassName = $"{workType}{_parameter.EntityName}CommandHandler";
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationModulePath, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandHandlerClassName} -o {_commandPath}");

            string _commandHandlerClassRoot = DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value;
            ClassService.SetBaseInheritance(_commandHandlerClassRoot, $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Result, {_parameter.EntityName}>");
            ClassService.AddUsing(_commandHandlerClassRoot, $"Core.Application.BaseCQRS.Commands.{workType}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value, _commandPath);

            string entityNameSpace = ClassService.GetNameSpace(_commandHandlerClassRoot).Value;
            ClassService.AddUsing(_commandHandlerClassRoot, entityNameSpace);
        }
    }
}
