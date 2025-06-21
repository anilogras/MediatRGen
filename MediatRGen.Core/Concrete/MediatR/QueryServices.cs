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
    internal class QueryServices
    {
        private readonly CreateServiceBaseSchema _parameter;
        private readonly ServicePaths _paths;
        IList<ClassConfiguration> _classConfigs;


        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;


        public QueryServices(
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

        public void CreateQueries()
        {

            string _applicationCommandsDirectoryPath = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries").Value;
            _directoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);


            CreateBaseQueryClasses("GetById");
            CreateBaseQueryClasses("GetList");
            CreateBaseQueryClasses("GetListDynamic");
            CreateBaseQueryClasses("GetListPaged");

        }

        private void CreateBaseQueryClasses(string workType)
        {

            string _queryPath = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _directoryServices.CreateIsNotExist(_queryPath);

            QueryConfiguration(workType);
            QueryHandlerConfiguration(workType);
            QueryResponseConfiguration(workType);

        }

        private void QueryConfiguration(string workType)
        {
            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _classConfig.Name = $"{workType}{_parameter.EntityName}Query";
            _classConfig.BaseInheritance = $"Base{workType}Query<{workType}{_parameter.EntityName}Response>";

            _classConfig.Usings = new List<string>
            {
                $"Core.Application.BaseCQRS.Queries.{workType}"
            };

            _classConfigs.Add(_classConfig);
        }

        private void QueryHandlerConfiguration(string workType)
        {

            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value; ;
            _classConfig.Name = $"{workType}{_parameter.EntityName}QueryHandler";
            _classConfig.BaseInheritance = $"Base{workType}QueryHandler<{workType}{_parameter.EntityName}Query, {workType}{_parameter.EntityName}Response, {_parameter.EntityName}>";

            _classConfig.Constructor = true;
            _classConfig.ConstructorParameters = $"IRepository<{_parameter.EntityName}> repository, IMapper mapper";
            _classConfig.ConstructorBaseParameters = "repository, mapper";

            _classConfig.Usings = new List<string>
            {
               "AutoMapper",
               "Core.Persistence.Repository",
               $"Core.Application.BaseCQRS.Queries.{workType}",
               _paths.EntityNamespace
            };

            _classConfigs.Add(_classConfig);

        }

        private void QueryResponseConfiguration(string workType)
        {
            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _classConfig.BaseInheritance = "IResponse";
            _classConfig.Usings = new List<string> { "Core.Application.BaseCQRS" };
            _classConfig.Name = $"{workType}{_parameter.EntityName}Response";
            _classConfigs.Add(_classConfig);
        }
    }
}
