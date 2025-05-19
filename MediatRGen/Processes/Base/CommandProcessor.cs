using MediatRGen.Cli.Helpers;

using MediatRGen.Cli.Processes.Config;
using MediatRGen.Cli.Processes.Module;
using MediatRGen.Cli.Processes.Solution;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatRGen.Cli.Processes.Service;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Core.Exceptions;


namespace MediatRGen.Cli.Processes.Base
{
    public static class CommandProcessor
    {

        public static BaseProcess ProcessHandler(string command)
        {
            string[] commandArgs = ArgHelpers.SplitArgs(command);
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
