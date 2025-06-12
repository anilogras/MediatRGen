using MediatRGen.Cli.Processes.Config;
using MediatRGen.Cli.Processes.Module;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Cli.Processes.Service;
using MediatRGen.Cli.Processes.Solution;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;

namespace MediatRGen.Cli.Processes.Base
{
    public static class CommandProcessor
    {

        public static BaseProcess ProcessHandler(string command)
        {
            string[] commandArgs = ArgsService.SplitArgs(command).Value;
            //Validator.ValidateCommandBeforeProcess(commandArgs);

            if (string.IsNullOrWhiteSpace(command))
            {
                throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }

            return commandArgs[0].ToLower() switch
            {
                "create-solution" => new CreateSolutionProcess(command),
                "create-config" => new CreateConfigProcess(),
                "update-config" => new UpdateConfigProcess(),
                "create-nuget" => new CreateNugetPackages(),
                "create-update" => new UpdateNugetPackageProcess(),
                "create-module" => new CreateModuleProcess(command),
                "create-service" => new CreateServiceProcess(command),
                "create-repository" => new RepositoryProcess(command),

                _ => throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName),
            };
        }

    }
}
