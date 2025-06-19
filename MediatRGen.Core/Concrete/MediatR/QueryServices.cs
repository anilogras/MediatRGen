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

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;


        public QueryServices(
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
            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}Query";
            _config.BaseInheritance = $"Base{workType}Query<{workType}{_parameter.EntityName}Response>";

            _config.Usings = new List<string>
            {
                $"Core.Application.BaseCQRS.Queries.{workType}"
            };

            _classService.CreateClass(_config);
        }

        private void QueryHandlerConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value; ;
            _config.Name = $"{workType}{_parameter.EntityName}QueryHandler";
            _config.BaseInheritance = $"Base{workType}QueryHandler<{workType}{_parameter.EntityName}Query, {workType}{_parameter.EntityName}Response, {_parameter.EntityName}>";
            
            _config.Constructor = true;
            _config.ConstructorParameters = $"IRepository<{_parameter.EntityName}> repository, IMapper mapper";
            _config.ConstructorBaseParameters = "repository, mapper";

            string _entityNamespace = _nameSpaceService.GetNameSpace(_classService.GetClassRoot(_paths.EntityPath).Value).Value;

            _config.Usings = new List<string>
            {
               "AutoMapper",
               "Core.Persistence.Repository",
               $"Core.Application.BaseCQRS.Queries.{workType}",
               _entityNamespace
            };

            _classService.CreateClass(_config);

        }

        private void QueryResponseConfiguration(string workType)
        {
            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = _directoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _config.BaseInheritance = "IResponse";
            _config.Usings = new List<string> { "Core.Application.BaseCQRS" };
            _config.Name = $"{workType}{_parameter.EntityName}Response";
            _classService.CreateClass(_config);
        }
    }
}
