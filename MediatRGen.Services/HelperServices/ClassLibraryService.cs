using MediatRGen.Core.Exceptions;
using MediatRGen.Core.States;
using MediatRGen.Services.Base;

namespace MediatRGen.Services.HelperServices
{
    public static class ClassLibraryService
    {
        public static ServiceResult<bool> Create(string name, string path)
        {
            try
            {
                string _srcPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "src").Value;
                DirectoryServices.CreateIsNotExist(_srcPath);
                name = CreateClassLibraryName(name).Value;
                path = DirectoryServices.GetCurrentDirectory().Value + "src/" + path;

                string res1 = SystemProcessHelpers.InvokeCommand($"dotnet new classlib -n {name} -o {path}/{name}");
                Console.WriteLine(res1);

                string res2 = SystemProcessHelpers.InvokeCommand($"dotnet sln {DirectoryServices.GetCurrentDirectory().Value}{GlobalState.Instance.ProjectName}.sln add {path}/{name}/{name}.csproj");
                Console.WriteLine(res2);

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().ClassLibraryCreated, null);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassLibraryCreateError, new ClassLibraryException(ex.Message));
            }
        }

        private static ServiceResult<string> CreateClassLibraryName(string moduleName)
        {
            return new ServiceResult<string>(GlobalState.Instance.SolutionName + "." + moduleName, true, LangHandler.Definitions().ClassLibraryNameCreated);
        }
    }
}
