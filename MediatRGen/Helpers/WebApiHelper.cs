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

            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), "src");

            name = CreateClassLibraryName(name);
            path = DirectoryHelpers.GetCurrentDirectory() + "src/" + path;

            string res1 = SystemProcessHelpers.InvokeCommand($"dotnet new webapi -n {name} -o {path}/{name}");
            Console.WriteLine(res1);
            string res2 = SystemProcessHelpers.InvokeCommand($"dotnet sln {DirectoryHelpers.GetCurrentDirectory()}{GlobalState.Instance.ProjectName}.sln add {path}/{name}/{name}.csproj");
            Console.WriteLine(res2);

            Console.WriteLine(LangHandler.Definitions().WebApiCreated + $" {name}");
        }

        private static string CreateClassLibraryName(string moduleName)
        {
            return GlobalState.Instance.SolutionName + "." + moduleName;
        }

    }
}
