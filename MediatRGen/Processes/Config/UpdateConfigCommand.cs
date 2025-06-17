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
        private readonly IConfigService _configService;

        public UpdateConfigCommand(ISettings settings, IConfigService configService)
        {
            _settings = settings;
            _configService = configService;
        }

        public override int Execute(CommandContext context)
        {
            _configService.Update();
            _settings.Update();
            return 0;
        }
    }
}
