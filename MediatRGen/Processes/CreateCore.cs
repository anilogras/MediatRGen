using MediatRGen.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class CreateCore
    {
        public CreateCore()
        {
            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), "Core");
            ClassLibraryHelpers.Create("Core.Application", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            ClassLibraryHelpers.Create("Core.CrossCuttingConcerns", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
            ClassLibraryHelpers.Create("Core.Persistence", PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), "Core"));
        }
    }
}
