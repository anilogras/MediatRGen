using MediatRGen.Services.Base;
using MediatRGen.Services.HelperServices;

namespace MediatRGen.Services.NugetServices
{
    public class NugetService
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
