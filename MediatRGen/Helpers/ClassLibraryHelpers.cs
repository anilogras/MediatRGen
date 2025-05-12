using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
{
    public class ClassLibraryHelpers
    {
        public static void Create(string name, string path)
        {
            DirectoryHelpers.CreateIsNotExist(path, name);
            SystemProcessHelpers.InvokeCommand($"dotnet new classlib -n {name} -o {path}\\{name}");
            SystemProcessHelpers.InvokeCommand($"dotnet sln {DirectoryHelpers.GetCurrentDirectory()}\\{GlobalState.Instance.ProjectName}.sln add {path}\\{name}\\{name}.csproj");
            Console.WriteLine(LangHandler.Definitions().ClassLibraryCreated + $" {name}");
        }

        public static string CreateClassLibraryName(string moduleName)
        {
            return GlobalState.Instance.SolutionName + "." + moduleName;
        }
    }
}
