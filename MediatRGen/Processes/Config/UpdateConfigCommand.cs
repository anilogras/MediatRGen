using MediatRGen.Cli.States;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Config
{
    internal class UpdateConfigCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            new CreateConfigCommand();
            GlobalState.UpdateInstance();
        }
    }
}
