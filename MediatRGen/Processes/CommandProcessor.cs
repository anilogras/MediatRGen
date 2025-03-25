using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public static class CommandProcessor
    {

        public static BaseProcess ProcessHandler(string command)
        {
            string[] commandArgs = ArgHelpers.SplitArgs(command);
            Validator.ValidateCommandBeforeProcess(commandArgs);

            if (string.IsNullOrWhiteSpace(command))
            {
                throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }

            return commandArgs[0].ToLower() switch
            {
                "create-solution" => new SolutionCreateProcess(command),
                "create-repository" => new RepositoryProcess(command),
                "create-config" => new ConfigCreateProcess(), 
                _ => throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName),
            };
        }

    }
}
