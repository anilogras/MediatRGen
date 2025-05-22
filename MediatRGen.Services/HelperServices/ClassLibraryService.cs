using MediatRGen.Core.Exceptions;
using MediatRGen.Services.Base;

namespace MediatRGen.Services.HelperServices
{
    public static class ClassLibraryService
    {
        public static ServiceResult<bool> Create(string name, string path, string projectName, string solutionName)
        {
            try
            {
                string _srcPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "src").Value;
                DirectoryServices.CreateIsNotExist(_srcPath);
                name = CreateClassLibraryName(name, solutionName).Value;
                path = DirectoryServices.GetCurrentDirectory().Value + "src/" + path;

                SystemProcessService.InvokeCommand($"dotnet new classlib -n {name} -o {path}/{name}");

                SystemProcessService.InvokeCommand($"dotnet sln {DirectoryServices.GetCurrentDirectory().Value}{projectName}.sln add {path}/{name}/{name}.csproj");

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().ClassLibraryCreated, null);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassLibraryCreateError, new ClassLibraryException(ex.Message));
            }
        }

        private static ServiceResult<string> CreateClassLibraryName(string moduleName, string solutionName)
        {
            return new ServiceResult<string>(solutionName + "." + moduleName, true, LangHandler.Definitions().ClassLibraryNameCreated);
        }
    }
}
