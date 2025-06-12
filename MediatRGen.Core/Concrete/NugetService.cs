using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class NugetService : INugetService
    {

        private readonly IDirectoryServices _directoryServices;

        public NugetService(IDirectoryServices directoryServices)
        {
            _directoryServices = directoryServices;
        }

        public ServiceResult<bool> DeleteNugets()
        {
            string _nugetFoldersDirectory = _directoryServices.GetPath(_directoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;
            ServiceResult<bool> deleteRes = _directoryServices.Delete(_nugetFoldersDirectory);
            return new ServiceResult<bool>(true, true, LangHandler.Definitions().NugetPackageDeleted);
        }
    }
}
