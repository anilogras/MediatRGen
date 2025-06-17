using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Core
{
    public class CreateCoreCommand : Command
    {

        private readonly ICoreServices _coreServices;

        public CreateCoreCommand(ICoreServices coreServices)
        {
            _coreServices = coreServices;
        }

        public override int Execute(CommandContext context)
        {
            _coreServices.Create();
            return 0;
        }
    }
}
