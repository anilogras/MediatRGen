using MediatRGen.Cli.Processes.Base;
using MediatRGen.Core;
using MediatRGen.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class UpdateNugetPackageProcess : BaseProcess
    {
        public UpdateNugetPackageProcess()
        {
            CreateCoreNugetPackages();
        }


        private static void CreateCoreNugetPackages()
        {
            DirectoryHelpers.Delete(DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "CoreNugetPackages"));

            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory()), "CoreNugetPackages");

            Assembly.GetExecutingAssembly().Location.ToString();

            string _rootpath =
                Assembly.GetExecutingAssembly().Location.Substring
                    (0, Assembly.GetExecutingAssembly().Location.ToString().LastIndexOf('\\'));

            string[] _nugetPackages = Directory.GetFiles($"{_rootpath}/nugetpackages");

            string _copyDestination = DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "CoreNugetPackages");

            foreach (var item in _nugetPackages)
            {
                string _fileName = Path.GetFileName(item);
                string _copyPath = Path.Combine(_copyDestination, _fileName);
                Console.WriteLine(_fileName + LangHandler.Definitions().NugetPackageCreated);
                File.Copy(item, _copyPath, true);
            }
        }
    }
}
