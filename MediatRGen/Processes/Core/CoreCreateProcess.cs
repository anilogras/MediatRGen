using MediatRGen.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Core
{
    public class CoreCreateProcess
    {
        public CoreCreateProcess()
        {
            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), "Core");
            ClassLibraryHelpers.Create("Core.Application", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            ClassLibraryHelpers.Create("Core.CrossCuttingConcerns", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            ClassLibraryHelpers.Create("Core.Persistence", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
        }
    }
}
