using MediatRGen.Core.Helpers;
using MediatRGen.Core.States;
using MediatRGen.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.HelperServices
{
    public class WebApiService
    {
        public static ServiceResult<bool> Create(string name, string path)
        {

            try
            {
                string _directory = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "src").Value;
                DirectoryServices.CreateIsNotExist(_directory);

                name = CreateClassLibraryName(name);
                path = DirectoryServices.GetCurrentDirectory().Value + "src/" + path;

                string res1 = SystemProcessHelpers.InvokeCommand($"dotnet new webapi -n {name} -o {path}/{name}");
                Console.WriteLine(res1);
                string res2 = SystemProcessHelpers.InvokeCommand($"dotnet sln {DirectoryServices.GetCurrentDirectory().Value}{GlobalState.Instance.ProjectName}.sln add {path}/{name}/{name}.csproj");
                Console.WriteLine(res2);

                return new ServiceResult<bool>(true, true, LangHandler.Definitions().WebApiCreated + $" {name}");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().WebApiCreateError + $" {name}", ex);

            }

        }

        private static ServiceResult<string> CreateClassLibraryName(string moduleName)
        {
            return new ServiceResult<string>(GlobalState.Instance.SolutionName + "." + moduleName, true, "");
        }
    }
}
