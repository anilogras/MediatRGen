using MediatRGen.Cli.Processes.MediatR;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete.MediatR
{
    public class ControllerService
    {
        private readonly CreateServiceBaseSchema _parameter;
        private readonly ServicePaths _paths;
        IList<ClassConfiguration> _classConfigs;

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;

        public ControllerService(
            CreateServiceBaseSchema parameter,
            ServicePaths paths,
            IList<ClassConfiguration> classConfigs,
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


        public void CreateController()
        {
            _directoryServices.CreateIsNotExist(_paths.ControllerDirectory);
            ControllerConfiguration();
        }

        private void ControllerConfiguration()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _paths.ControllerDirectory;
            _classConfig.Name = $"{_paths.EntityNameNotExt}Controller";
            _classConfig.BaseInheritance = $"Controller";

            _classConfig.Usings = new List<string>
            {
                $"Microsoft.AspNetCore.Mvc"
            };

            _classConfigs.Add(_classConfig);
        }
    }
}
