using MediatRGen.Cli.Languages;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Helpers
{
    internal class WebApiHelper
    {
        public static void Create(string name, string path)
        {

            DirectoryHelpers.CreateIsNotExist("./", "src");

            name = CreateClassLibraryName(name);
            path = "./src/" + path;

            SystemProcessHelpers.InvokeCommand($"dotnet new webapi -n {name} -o {path}/{name}");
            SystemProcessHelpers.InvokeCommand($"dotnet sln {GlobalState.Instance.ProjectName}.sln add {path}/{name}/{name}.csproj");
            SystemProcessHelpers.InvokeCommand($"dotnet build {GlobalState.Instance.ProjectName}.sln");
            Console.WriteLine(LangHandler.Definitions().ClassLibraryCreated + $" {name}");
        }

        private static string CreateClassLibraryName(string moduleName)
        {
            return GlobalState.Instance.SolutionName + "." + moduleName;
        }

    }
}
