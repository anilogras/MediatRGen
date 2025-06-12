using MediatRGen.Cli.Processe2s.Nuget;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class CreateNugetCommand : Command
    {
        public override int Execute(CommandContext context)
        {

            new UpdateNugetPackageProcess();

        }
    }
}
