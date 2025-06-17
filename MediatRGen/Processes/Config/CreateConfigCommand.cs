using MediatRGen.Cli.Processes.Core;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Config
{
    public class CreateConfigCommand : Command
    {
        private readonly IConfigService _configService;

        public CreateConfigCommand(IConfigService configService)
        {
            _configService = configService;
        }

        public override int Execute(CommandContext context)
        {

            _configService.Create();
            return 0;
        }


    }
}
