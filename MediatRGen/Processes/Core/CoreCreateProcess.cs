using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Core
{
    public class CoreCreateProcess
    {
        public CoreCreateProcess()
        {
            ClassLibraryHelpers.Create("Core.Persistence", "Core");
            SystemProcessHelpers.BuildProject();
        }

    }
}
