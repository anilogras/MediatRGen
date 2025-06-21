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
        IList<ClassConfiguration> _classConfigs;

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;

        public CommandServices(
            CreateServiceBaseSchema parameter,
            ServicePaths paths,
            IList<ClassConfiguration> classConfigs ,
            IDirectoryServices directoryServices,
            IClassService classService,
            INameSpaceService nameSpaceService)
        {
            _parameter = parameter;
            _paths = paths;
            _directoryServices = directoryServices;
            _classService = classService;
            _nameSpaceService = nameSpaceService;
            _classConfigs = classConfigs;
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

            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _classConfig.Name = $"{workType}{_parameter.EntityName}Command";
            _classConfig.BaseInheritance = $"Base{workType}Command<{workType}{_parameter.EntityName}Response>";
            _classConfig.Constructor = true;

            _classConfig.Usings = new List<string> { $"Core.Application.BaseCQRS.Commands.{workType}", _paths.EntityNamespace };

            _classConfigs.Add(_classConfig);

            ValidatiorConfiguration(workType);
        }

        private void ValidatiorConfiguration(string workType)
        {
            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value; ;
            _classConfig.Name = $"{workType}{_paths.EntityName}CommandValidator";
            _classConfig.BaseInheritance = $"AbstractValidator<{workType}{_paths.EntityName}Command>";
            _classConfig.Constructor = true;
            _classConfig.Usings = new List<string>
            {
                "FluentValidation"
            };

            _classConfigs.Add(_classConfig);

        }


        private void CommandHandlerConfiguration(string workType)
        {

            ClassConfiguration _classConfig = new ClassConfiguration();

            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _classConfig.Name = $"{workType}{_parameter.EntityName}CommandHandler";
            _classConfig.BaseInheritance = $"Base{workType}CommandHandler<{workType}{_parameter.EntityName}Command, {workType}{_parameter.EntityName}Response, {_parameter.EntityName}>";
            _classConfig.Constructor = true;
            _classConfig.ConstructorParameters = $"IRepository<{_parameter.EntityName}> repository, IMapper mapper";
            _classConfig.ConstructorBaseParameters = "repository, mapper";

            _classConfig.Usings = new List<string>
            {
                "AutoMapper",
                "Core.Persistence.Repository",
                $"Core.Application.BaseCQRS.Commands.{workType}" ,
                _paths.EntityNamespace
            };

            _classConfigs.Add(_classConfig);
        }

        private void CommandResponseConfiguration(string workType)
        {
            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Commands", workType).Value;
            _classConfig.BaseInheritance = "IResponse";
            _classConfig.Usings = new List<string> { "Core.Application.BaseCQRS" };
            _classConfig.Name = $"{workType}{_parameter.EntityName}Response";
            _classConfigs.Add(_classConfig);
        }
    }
}
