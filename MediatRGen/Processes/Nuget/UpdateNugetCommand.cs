using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Nuget
{
    internal class UpdateNugetCommand : Command
    {
        private readonly INugetService _nugetServices;

        public UpdateNugetCommand(INugetService nugetServices)
        {
            _nugetServices = nugetServices;
        }

        public override int Execute(CommandContext context)
        {

            _nugetServices.CreateNugets();
            return 0;
        }
    }
}
