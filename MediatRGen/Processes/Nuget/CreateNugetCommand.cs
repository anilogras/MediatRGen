using Spectre.Console.Cli;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class CreateNugetCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            new UpdateNugetCommand();
        }
    }
}
