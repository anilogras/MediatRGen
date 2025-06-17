using MediatRGen.Cli.Schemas;
using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Processes.Solution
{
    public class CreateSolutionCommand : Command<CreateSolutionSchema>
    {
        private readonly ISolutionService _solutionService;

        public CreateSolutionCommand(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        public override int Execute(CommandContext context, CreateSolutionSchema settings)
        {
            var options = settings.OptionsSet();
            _solutionService.Create(options);
            return 0;
        }
    }
}
