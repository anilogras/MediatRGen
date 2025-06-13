using MediatRGen.Cli.States;
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

        private readonly IClassLibraryService _classLibraryService;
        private readonly ISystemProcessService _systemProcessService;
        private readonly ISettings _settings;

        public CreateCoreCommand(IClassLibraryService classLibraryService, ISystemProcessService systemProcessService)
        {
            _classLibraryService = classLibraryService;
            _systemProcessService = systemProcessService;
        }


        public override int Execute(CommandContext context)
        {
            _classLibraryService.Create("Core.Persistence", "Core", _settings.ProjectName, _settings.SolutionName);
            _systemProcessService.BuildProject(_settings.ProjectName);
            return 0;
        }
    }
}
