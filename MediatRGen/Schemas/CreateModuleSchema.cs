using CommandLine;
using MediatRGen.Core.Schemas;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Schemas
{
    public class CreateModuleSchema : CommandSettings
    {
        [CommandOption("-n|--name")]
        public string ModuleName { get; set; }


        public CreateModuleBaseSchema OptionsSet() => new CreateModuleBaseSchema
        {
            ModuleName = ModuleName,
        };

    }
}
