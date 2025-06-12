using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class WebApiService : IWebApiService
    {

        private readonly IDirectoryServices _directoryServices;
        private readonly ISystemProcessService _systemProcessService;

        public WebApiService(IDirectoryServices directoryServices , ISystemProcessService systemProcessService)
        {
            _directoryServices = directoryServices;
            _systemProcessService = systemProcessService;
        }

        public ServiceResult<bool> Create(string name, string path, string projectName, string solutionName)
        {

            try
            {
                string _directory = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "src").Value;
                _directoryServices.CreateIsNotExist(_directory);

                name = CreateClassLibraryName(name, solutionName).Value;
                path = _directoryServices.GetCurrentDirectory().Value + "src/" + path;

                _systemProcessService.InvokeCommand($"dotnet new webapi -n {name} -o {path}/{name}");

                _systemProcessService.InvokeCommand($"dotnet sln {_directoryServices.GetCurrentDirectory().Value}{projectName}.sln add {path}/{name}/{name}.csproj");

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().WebApiCreated + $" {name}");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().WebApiCreateError + $" {name}", ex);

            }

        }
        private ServiceResult<string> CreateClassLibraryName(string moduleName, string solutionName)
        {
            return new ServiceResult<string>(solutionName + "." + moduleName, true, "");
        }
    }
}
