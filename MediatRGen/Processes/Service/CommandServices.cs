using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Services.HelperServices;
using MediatRGen.Services.Models;
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
            string _applicationCommandsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Commands").Value;
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
            string _commandPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            DirectoryServices.CreateIsNotExist(_commandPath);

            CommandConfiguration(workType);
            CommandHandlerConfiguration(workType);
            ResultConfiguration(workType);
        }



        private void ResultConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Results").Value;
            _config.Name = $"{workType}{_paths.EntityNameNotExt}Result";
            _config.BaseInheritance = "IResponse";
            _config.Usings = new List<string> { "Core.Application.BaseCQRS" };

            ClassService.CreateClass(_config);

        }

        private void CommandConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}Command";
            _config.BaseInheritance = $"Base{workType}Command<{_parameter.EntityName}>";

            string _entityNamespace = ClassService.GetNameSpace(_paths.EntityPath).Value;
            _config.Usings = new List<string> { $"Core.Application.BaseCQRS.Commands.{workType}", _entityNamespace };

            ClassService.CreateClass(_config);

            ValidatiorConfiguration(workType, _config.Directory);
        }

        private void ValidatiorConfiguration(string workType, string _commandPath)
        {
            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = _commandPath;
            _config.Name = $"{workType}{_paths.EntityNameNotExt}CommandValidator";
            _config.BaseInheritance = $"AbstractValidator<{workType}{_paths.EntityNameNotExt}Command>";
            _config.Constructor = true;
            _config.Usings = new List<string>
            {
                "FluentValidation"
            };

            ClassService.CreateClass(_config);

        }


        private void CommandHandlerConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}CommandHandler";
            _config.BaseInheritance = $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Result, {_parameter.EntityName}>";

            string _entityNamespace = ClassService.GetNameSpace(_paths.EntityPath).Value;

            _config.Usings = new List<string>
            {
                $"Core.Application.BaseCQRS.Commands.{workType}" ,
                _entityNamespace ,
                $"{_paths.ApplicationDirectory}.Results"
            };

            ClassService.CreateClass(_config);
        }
    }
}
