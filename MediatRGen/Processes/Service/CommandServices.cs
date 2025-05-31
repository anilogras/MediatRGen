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
            string _applicationCommandsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands").Value;
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
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            DirectoryServices.CreateIsNotExist(_commandPath);

            CommandConfiguration(workType);
            CommandHandlerConfiguration(workType);
            ResultConfiguration(workType);
        }

        

        private void ResultConfiguration(string workType)
        {
            string _resultsPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Results").Value;
            DirectoryServices.CreateIsNotExist(_resultsPath);

            string _resultClassName = $"{workType}{_paths.EntityNameNotExt}Result";

            SystemProcessService.InvokeCommand($"dotnet new class -n {_resultClassName} -o {_resultsPath}");
            
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_resultsPath , _resultClassName).Value, _resultsPath);
            ClassService.SetBaseInheritance(DirectoryServices.GetPath(_resultsPath, _resultClassName).Value, $"IResponse");
            
            ClassService.AddUsing(DirectoryServices.GetPath(_resultsPath, _resultClassName).Value, "Core.Application.BaseCQRS");
        }

        private void CommandConfiguration(string workType)
        {
            string _commandClassName = $"{workType}{_parameter.EntityName}Command";
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandClassName} -o {_commandPath}");

            string _commandClassRoot = DirectoryServices.GetPath(_commandPath, _commandClassName).Value;
            ClassService.SetBaseInheritance(_commandClassRoot, $"Base{workType}Command<{_parameter.EntityName}>");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandClassName).Value, _commandPath);

            ClassService.AddUsing(_commandClassRoot, $"Core.Application.BaseCQRS.Commands.{workType}");


            string _entityNamespace = ClassService.GetNameSpace(_paths.EntityPath).Value;
            ClassService.AddUsing(_commandClassRoot, _entityNamespace);

            ValidatiorConfiguration(workType, _commandPath);

        }

        private void ValidatiorConfiguration(string workType, string _commandPath)
        {
            string _validatorClassName = $"{workType}{_paths.EntityNameNotExt}CommandValidator";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_validatorClassName} -o {_commandPath}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _validatorClassName).Value, _commandPath);
            ClassService.SetBaseInheritance(DirectoryServices.GetPath(_commandPath, _validatorClassName).Value, $"AbstractValidator<{workType}{_paths.EntityNameNotExt}Command>");
            ClassService.AddConstructor(DirectoryServices.GetPath(_commandPath, _validatorClassName).Value);
            ClassService.AddUsing(DirectoryServices.GetPath(_commandPath, _validatorClassName).Value, "FluentValidation");

        }


        private void CommandHandlerConfiguration(string workType)
        {

            string _commandHandlerClassName = $"{workType}{_parameter.EntityName}CommandHandler";
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Commands", workType).Value;
            SystemProcessService.InvokeCommand($"dotnet new class -n {_commandHandlerClassName} -o {_commandPath}");

            string _commandHandlerClassRoot = DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value;
            ClassService.SetBaseInheritance(_commandHandlerClassRoot, $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Result, {_parameter.EntityName}>");
            ClassService.AddUsing(_commandHandlerClassRoot, $"Core.Application.BaseCQRS.Commands.{workType}");
            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_commandPath, _commandHandlerClassName).Value, _commandPath);

            string _entityNamespace = ClassService.GetNameSpace(_paths.EntityPath).Value;
            ClassService.AddUsing(_commandHandlerClassRoot, _entityNamespace);
        }
    }
}
