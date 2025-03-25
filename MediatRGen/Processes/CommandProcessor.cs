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
            string[] commandArgs = ArgHelper.SplitArgs(command);
            Validator.ValidateCommandBeforeProcess(commandArgs);

            if (string.IsNullOrWhiteSpace(command))
            {
                throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }

            switch (commandArgs[0].ToLower())
            {
                case "create-solution":
                    return new SolutionCreateProcess(command);

                case "create-repository":
                    return new RepositoryProcess(command);

                default:
                    throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }
        }

    }
}
