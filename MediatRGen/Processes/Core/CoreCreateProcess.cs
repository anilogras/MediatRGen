using MediatRGen.Cli.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Core
{
    public class CoreCreateProcess
    {
        public CoreCreateProcess()
        {

            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory()), "CoreNugetPackages");

            string[] _nugetPackages = Directory.GetFiles("./nugetpackages");

            string _copyDestination = DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory() + "CoreNugetPackages");

            foreach (var item in _nugetPackages)
            {
                string _fileName = Path.GetFileName(item);
                string _copyPath = Path.Combine(_copyDestination, _fileName);
                File.Copy(item, _copyPath, true);
            }


            //ClassLibraryHelpers.Create("Core.Application", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            //ClassLibraryHelpers.Create("Core.CrossCuttingConcerns", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            //ClassLibraryHelpers.Create("Core.Persistence", DirectoryHelpers.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
        }
    }
}
