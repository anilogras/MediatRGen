using MediatRGen.Cli.Processes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class CreateNugetPackages : BaseProcess
    {
        public CreateNugetPackages()
        {
            new UpdateNugetPackageProcess();
        }
    }
}
