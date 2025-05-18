using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.Processes.Config;
using MediatRGen.Cli.Processes.Module;
using MediatRGen.Cli.Processes.Solution;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                "create-solution" => new CreateSolution(command),
                "create-repository" => new RepositoryProcess(command),
                "create-config" => new CreateConfigProcess(),
                "update-config" => new UpdateConfigProcess(),
                "create-module" => new CreateModuleProcess(command),
                _ => throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName),
            };
        }

    }
}
