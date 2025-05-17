using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Core
{
    public class CoreCreateProcess
    {
        public CoreCreateProcess()
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


            //ClassLibraryHelpers.Create("Core.Application", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            //ClassLibraryHelpers.Create("Core.CrossCuttingConcerns", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            //ClassLibraryHelpers.Create("Core.Persistence", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
        }
    }
}
