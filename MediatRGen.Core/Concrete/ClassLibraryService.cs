using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    public class ClassLibraryService : IClassLibraryService
    {
        private readonly IDirectoryServices _directoryServices;
        private readonly ISystemProcessService _systemProcessService;

        public ClassLibraryService(IDirectoryServices directoryService , ISystemProcessService systemProcessService)
        {
            _directoryServices = directoryService;
            _systemProcessService = systemProcessService;
        }

        public ServiceResult<bool> Create(string name, string path, string projectName, string solutionName)
        {
            try
            {
                string _srcPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "src").Value;
                _directoryServices.CreateIsNotExist(_srcPath);
                name = CreateClassLibraryName(name, solutionName).Value;
                path = _directoryServices.GetCurrentDirectory().Value + "src/" + path;

                _systemProcessService.InvokeCommand($"dotnet new classlib -n {name} -o {path}/{name}");
                _systemProcessService.InvokeCommand($"dotnet sln {_directoryServices.GetCurrentDirectory().Value}{projectName}.sln add {path}/{name}/{name}.csproj");

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().ClassLibraryCreated, null);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassLibraryCreateError, new ClassLibraryException(ex.Message));
            }
        }
        private ServiceResult<string> CreateClassLibraryName(string moduleName, string solutionName)
        {
            return new ServiceResult<string>(solutionName + "." + moduleName, true, LangHandler.Definitions().ClassLibraryNameCreated);
        }
    }
}
