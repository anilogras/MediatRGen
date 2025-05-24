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
        public QueryServices(ServiceCreateParameter parameter)
        {
            _parameter = parameter;
        }

        public void CreateQueries(string _entityPath, string _applicationModulePath, string _pluralEntityName)
        {

            string _applicationQueriesDirectoryPath = _applicationModulePath + _entityPath + _pluralEntityName + "\\Queries";
            DirectoryServices.CreateIsNotExist(_applicationQueriesDirectoryPath);

            GetByIdQuery(_entityPath, _applicationModulePath, _pluralEntityName, _applicationQueriesDirectoryPath);
            GetListQuery(_entityPath, _applicationModulePath, _pluralEntityName, _applicationQueriesDirectoryPath);
            GetListDynamicQuery(_entityPath, _applicationModulePath, _pluralEntityName, _applicationQueriesDirectoryPath);

        }

        private void GetByIdQuery(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseQueryClasses(fullPath, "GetById");
        }

        private void GetListQuery(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseQueryClasses(fullPath, "GetList");
        }

        private void GetListDynamicQuery(string _entityPath, string _applicationModulePath, string _pluralEntityName, string fullPath)
        {
            CreateBaseQueryClasses(fullPath, "GetListByDynamic");
        }


        private void CreateBaseQueryClasses(string fullPath, string name)
        {
            string _createCommandPath = fullPath + $"\\{name}";
            DirectoryServices.CreateIsNotExist(_createCommandPath);

            string _createCommandClassName = $"{name}{_parameter.EntityName}Query";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_createCommandClassName} -o {_createCommandPath}");

            string _createCommandHandlerClassName = $"{name}{_parameter.EntityName}QueryHandler";
            SystemProcessService.InvokeCommand($"dotnet new class -n {_createCommandHandlerClassName} -o {_createCommandPath}");
        }


    }
}
