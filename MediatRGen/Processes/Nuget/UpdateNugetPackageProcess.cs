using MediatRGen.Cli.Processes.Base;
using MediatRGen.Services;
using MediatRGen.Services.HelperServices;
using MediatRGen.Services.NugetServices;
using System.Reflection;

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

            NugetService _nugetService = new NugetService();
            //_nugetService.DeleteNugets();

            string _newPath = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

            DirectoryServices.CreateIsNotExist(_newPath);

            string _rootpath =
                Assembly.GetExecutingAssembly().Location.Substring
                    (0, Assembly.GetExecutingAssembly().Location.ToString().LastIndexOf('\\'));

            string[] _nugetPackages = Directory.GetFiles($"{_rootpath}/nugetpackages");

            string _copyDestination = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

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
