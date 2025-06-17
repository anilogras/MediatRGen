using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System.Reflection;

namespace MediatRGen.Core.Concrete
{
    internal class NugetService : INugetService
    {

        private readonly IDirectoryServices _directoryServices;
        private readonly IOutputService _outputService;
        public NugetService(IDirectoryServices directoryServices, IOutputService outputService)
        {
            _directoryServices = directoryServices;
            _outputService = outputService;
        }

        public ServiceResult<bool> CreateNugets()
        {
            try
            {
                string _newPath = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

                _directoryServices.CreateIsNotExist(_newPath);

                string _rootpath =
                    Assembly.GetExecutingAssembly().Location.Substring
                        (0, Assembly.GetExecutingAssembly().Location.ToString().LastIndexOf('\\'));

                string[] _nugetPackages = Directory.GetFiles($"{_rootpath}/nugetpackages");

                string _copyDestination = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;

                foreach (var item in _nugetPackages)
                {
                    string _fileName = Path.GetFileName(item);
                    string _copyPath = Path.Combine(_copyDestination, _fileName);
                    _outputService.Info(_fileName + LangHandler.Definitions().NugetPackageCreated);
                    File.Copy(item, _copyPath, true);
                }

                return new ServiceResult<bool>(true, true, "");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, ex.Message);
            }
        }

        public ServiceResult<bool> DeleteNugets()
        {
            string _nugetFoldersDirectory = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;
            ServiceResult<bool> deleteRes = _directoryServices.Delete(_nugetFoldersDirectory);
            return deleteRes;
        }
    }
}
