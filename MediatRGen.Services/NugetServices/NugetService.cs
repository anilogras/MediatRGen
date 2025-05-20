using MediatRGen.Core;
using MediatRGen.Core.Helpers;
using MediatRGen.Services.Base;
using MediatRGen.Services.HelperServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
