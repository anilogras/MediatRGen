using MediatRGen.Cli.Processes.Parameters.Services;
using MediatRGen.Services.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Service
{
    internal class QueryServices
    {
        private readonly ServiceCreateParameter _parameter;
        private readonly ServicePaths _paths;

        public QueryServices(ServiceCreateParameter parameter, ServicePaths paths)
        {
            _parameter = parameter;
            _paths = paths;
        }

        private string _entityRoot;

        public void CreateQueries()
        {

            string _applicationCommandsDirectoryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Queries").Value;
            DirectoryServices.CreateIsNotExist(_applicationCommandsDirectoryPath);


            CreateBaseQueryClasses("GetById");
            CreateBaseQueryClasses("GetList");
            CreateBaseQueryClasses("GetListDynamic");
            CreateBaseQueryClasses("GetListPaged");

        }

        private void CreateBaseQueryClasses(string workType)
        {

            string _queryPath = DirectoryServices.GetPath(_paths.ApplicationDirectory, _paths.EntityLocalDirectory, _paths.EntityPluralName, "Queries", workType).Value;
            DirectoryServices.CreateIsNotExist(_queryPath);

            QueryConfiguration(workType, _queryPath);
            QueryHandlerConfiguration(workType, _queryPath);
        }

        private void QueryConfiguration(string workType, string _queryPath)
        {
            string _queryClassName = $"{workType}{_parameter.EntityName}Query";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_queryClassName} -o {_queryPath}");

            ClassService.SetBaseInheritance(_queryPath + "\\" + _queryClassName, $"Base{workType}Query<{_parameter.EntityName}>");
            ClassService.AddUsing(_queryPath + "\\" + _queryClassName, $"Core.Application.BaseCQRS.Queries.{workType}");

            ClassService.ChangeNameSpace(_queryPath + "\\" + _queryClassName, _queryPath);
            ClassService.AddUsing(_queryPath + "\\" + _queryClassName, _paths.EntityDirectory);
        }

        private void QueryHandlerConfiguration(string workType, string _queryPath)
        {
            string _queryHandlerHandlerClassName = $"{workType}{_parameter.EntityName}QueryHandler";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_queryHandlerHandlerClassName} -o {_queryPath}");

            string _queryHandlerClassRoot = DirectoryServices.GetPath(_queryPath, _queryHandlerHandlerClassName).Value;

            ClassService.ChangeNameSpace(DirectoryServices.GetPath(_queryPath, _queryHandlerHandlerClassName).Value, _queryPath);
            ClassService.SetBaseInheritance(_queryHandlerClassRoot, $"Base{workType}QueryHandler<{_parameter.EntityName}>");
            ClassService.AddUsing(_queryHandlerClassRoot, $"Core.Application.BaseCQRS.Queries.{workType}");


            string _entityNamespace = ClassService.GetNameSpace(_paths.EntityPath).Value;
            ClassService.AddUsing(_queryHandlerClassRoot, _entityNamespace);
        }
    }
}
