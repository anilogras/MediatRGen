using MediatRGen.Services.Base;

namespace MediatRGen.Services.HelperServices
{
    public class WebApiService
    {
        public static ServiceResult<bool> Create(string name, string path, string projectName, string solutionName)
        {

            try
            {
                string _directory = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "src").Value;
                DirectoryServices.CreateIsNotExist(_directory);

                name = CreateClassLibraryName(name, solutionName).Value;
                path = DirectoryServices.GetCurrentDirectory().Value + "src/" + path;

                SystemProcessService.InvokeCommand($"dotnet new webapi -n {name} -o {path}/{name}");

                SystemProcessService.InvokeCommand($"dotnet sln {DirectoryServices.GetCurrentDirectory().Value}{projectName}.sln add {path}/{name}/{name}.csproj");

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().WebApiCreated + $" {name}");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().WebApiCreateError + $" {name}", ex);

            }

        }

        private static ServiceResult<string> CreateClassLibraryName(string moduleName, string solutionName)
        {
            return new ServiceResult<string>(solutionName + "." + moduleName, true, "");
        }
    }
}
