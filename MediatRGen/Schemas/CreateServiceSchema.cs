using CommandLine;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Models
{
    public class CreateServiceSchema : CommandSettings
    {
        [CommandArgument(0, "<Entity>")]
        public string EntityName { get; set; }


        [CommandArgument(1, "<Module>")]
        public string ModuleName { get; set; }

    }
}
