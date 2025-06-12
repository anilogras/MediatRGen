using MediatRGen.Cli.Models;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.MediatR
{
    internal class QueryServices
    {
        private readonly CreateServiceSchema _parameter;
        private readonly ServicePaths _paths;

        public QueryServices(CreateServiceSchema parameter, ServicePaths paths)
        {
            _parameter = parameter;
            _paths = paths;
        }

        public void CreateQueries()
        {

            string _applicationCommandsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Queries").Value;
            DirectoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);


            CreateBaseQueryClasses("GetById");
            CreateBaseQueryClasses("GetList");
            CreateBaseQueryClasses("GetListDynamic");
            CreateBaseQueryClasses("GetListPaged");

        }

        private void CreateBaseQueryClasses(string workType)
        {

            string _queryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            DirectoryServices.CreateIsNotExist(_queryPath);

            QueryConfiguration(workType);
            QueryHandlerConfiguration(workType);
            QueryResponseConfiguration(workType);

        }

        private void QueryConfiguration(string workType)
        {
            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _config.Name = $"{workType}{_parameter.EntityName}Query";
            _config.BaseInheritance = $"Base{workType}Query<{_parameter.EntityName}>";
            _config.Usings = new List<string>
            {
                $"Core.Application.BaseCQRS.Queries.{workType}",
                _paths.EntityDirectory
            };

            ClassService.CreateClass(_config);
        }

        private void QueryHandlerConfiguration(string workType)
        {

            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value; ;
            _config.Name = $"{workType}{_parameter.EntityName}QueryHandler";
            _config.BaseInheritance = $"Base{workType}QueryHandler<{workType}{_parameter.EntityName}Query, {workType}{_parameter.EntityName}Response, {_parameter.EntityName}>";
            _config.Constructor = true;

            string _entityNamespace = NameSpaceService.GetNameSpace(_paths.EntityPath).Value;

            _config.Usings = new List<string>
            {
               $"Core.Application.BaseCQRS.Queries.{workType}",
               _entityNamespace
            };

            ClassService.CreateClass(_config);

        }

        private void QueryResponseConfiguration(string workType)
        {
            ClassConfiguration _config = new ClassConfiguration();
            _config.Directory = DirectoryServices.GetPath(_paths.ApplicationDirectory, "Queries", workType).Value;
            _config.BaseInheritance = "IResponse";
            _config.Usings = new List<string> { "Core.Application.BaseCQRS" };
            _config.Name = $"{workType}{_parameter.EntityName}Response";
            ClassService.CreateClass(_config);
        }
    }
}
