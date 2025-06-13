using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Nuget
{
    internal class UpdateNugetCommand : Command
    {

        private readonly IDirectoryServices _directoryService;


        public override int Execute(CommandContext context)
        {
            NugetService _nugetService = new NugetService();
            //_nugetService.DeleteNugets();

            string _newPath = _directoryService.GetPath(_directoryService.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

            _directoryService.CreateIsNotExist(_newPath);

            string _rootpath =
                Assembly.GetExecutingAssembly().Location.Substring
                    (0, Assembly.GetExecutingAssembly().Location.ToString().LastIndexOf('\\'));

            string[] _nugetPackages = Directory.GetFiles($"{_rootpath}/nugetpackages");

            string _copyDestination = _directoryService.GetPath(_directoryService.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

            foreach (var item in _nugetPackages)
            {
                string _fileName = Path.GetFileName(item);
                string _copyPath = Path.Combine(_copyDestination, _fileName);
                Console.WriteLine(_fileName + LangHandler.Definitions().NugetPackageCreated);
                File.Copy(item, _copyPath, true);
            }

            return 0;
        }
    }
}
