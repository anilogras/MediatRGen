using MediatRGen.Core.Concrete;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.MediatR
{
    internal class CommandServices
    {

        private readonly CreateServiceBaseSchema _parameter;
        private readonly ServicePaths _paths;

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;

        public CommandServices(
            CreateServiceBaseSchema parameter,
            ServicePaths paths,
            IDirectoryServices directoryServices,
            IClassService classService,
            INameSpaceService nameSpaceService)
        {
            _parameter = parameter;
            _paths = paths;
            _directoryServices = directoryServices;
            _classService = classService;
            _nameSpaceService = nameSpaceService;
        }


        public void CreateCommands()
        {
            string _applicationCommandsDirectoryPath = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands").Value;
            _directoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);

            CreateCommandClass("Create");
            CreateCommandClass("Delete");
            CreateCommandClass("Update");
        }

        private void CreateCommandClass(string workType)
        {
            string _commandPath = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _directoryServices.CreateIsNotExist(_commandPath);

            CommandConfiguration(workType);
            CommandHandlerConfiguration(workType);
            CommandResponseConfiguration(workType);
            //ResultConfiguration(workType);
        }

        private void CommandConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}Command";
            _config.BaseInheritance = $"Base{workType}Command<{workType}{_parameter.EntityName}Response>";
            _config.Constructor = true;

            string _entityNamespace = _nameSpaceService.GetNameSpace(_paths.EntityPath).Value;
            _config.Usings = new List<string> { $"Core.Application.BaseCQRS.Commands.{workType}", _entityNamespace };

            _classService.CreateClass(_config);

            ValidatiorConfiguration(workType);
        }

        private void ValidatiorConfiguration(string workType)
        {
            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value; ;
            _config.Name = $"{workType}{_paths.EntityNameNotExt}CommandValidator";
            _config.BaseInheritance = $"AbstractValidator<{workType}{_paths.EntityNameNotExt}Command>";
            _config.Constructor = true;
            _config.Usings = new List<string>
            {
                "FluentValidation"
            };

            _classService.CreateClass(_config);

        }


        private void CommandHandlerConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();

            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}CommandHandler";
            _config.BaseInheritance = $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Response, {_parameter.EntityName}>";
            _config.Constructor = true;
            _config.ConstructorParameters = $"IRepository<{_parameter.EntityName}> repository, IMapper mapper";
            _config.ConstructorBaseParameters = "repository, mapper";

            string _entityNamespace = _nameSpaceService.GetNameSpace(_paths.EntityPath).Value;

            _config.Usings = new List<string>
            {
                "AutoMapper",
                "Core.Persistence.Repository",
                $"Core.Application.BaseCQRS.Commands.{workType}" ,
                _entityNamespace
            };

            _classService.CreateClass(_config);
        }

        private void CommandResponseConfiguration(string workType)
        {
            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _config.BaseInheritance = "IResponse";
            _config.Usings = new List<string> { "Core.Application.BaseCQRS" };
            _config.Name = $"{workType}{_parameter.EntityName}Response";
            _classService.CreateClass(_config);
        }
    }
}
