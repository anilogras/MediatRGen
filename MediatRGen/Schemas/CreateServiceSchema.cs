using CommandLine;
using MediatRGen.Core.Schemas;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Schemas
{
    public class CreateServiceSchema : CommandSettings
    {
        [CommandOption("-e|--entity")]
        public string EntityName { get; set; }


        [CommandOption("-m|--module")]
        public string ModuleName { get; set; }

        public CreateServiceBaseSchema OptionsSet() => new CreateServiceBaseSchema
        {
            ModuleName = ModuleName,
            EntityName = EntityName
        };


    }
}
