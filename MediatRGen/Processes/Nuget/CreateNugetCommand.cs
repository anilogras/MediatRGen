using MediatRGen.Core.Services;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class CreateNugetCommand : Command
    {

        private readonly INugetService _nugetServices;

        public CreateNugetCommand(INugetService nugetServices)
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
