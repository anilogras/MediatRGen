using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Module
{
    public class CreateModuleCommand : Command<ServiceSchemas>
    {
        private readonly IModuleService _moduleService;

        public CreateModuleCommand(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public override int Execute(CommandContext context, ServiceSchemas settings)
        {
            _moduleService.Create(settings);
            return 0;
        }


    }
}
