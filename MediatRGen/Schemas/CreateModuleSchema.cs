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
        [CommandArgument(0, "<Name>")]
        public string ModuleName { get; set; }


        public CreateModuleBaseSchema OptionsSet() => new CreateModuleBaseSchema
        {
            ModuleName = ModuleName,
        };

    }
}
