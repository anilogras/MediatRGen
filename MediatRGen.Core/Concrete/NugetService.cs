using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class NugetService : INugetService
    {
        public NugetService()
        {

        }
        public ServiceResult<bool> DeleteNugets()
        {
            string _nugetFoldersDirectory = DirectoryServices.GetPath(DirectoryServices.GetCurrentDirectory().Value, "CoreNugetPackages").Value;
            ServiceResult<bool> deleteRes = DirectoryServices.Delete(_nugetFoldersDirectory);
            return new ServiceResult<bool>(true, true, LangHandler.Definitions().NugetPackageDeleted);
        }
    }
}
