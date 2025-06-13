using MediatRGen.Core.Services;
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

        private readonly ISettings _settings;

        public UpdateConfigCommand(ISettings settings)
        {
            _settings = settings;
        }

        public override int Execute(CommandContext context)
        {
            var app = new CommandApp();

            app.Configure(cnf =>
            {
                cnf.AddCommand<CreateConfigCommand>(context.Name);
            });

            app.Run(context.Arguments);

            _settings.Update();

            return 0;
        }
    }
}
