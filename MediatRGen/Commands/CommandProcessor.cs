using MediatRGen.Exceptions;
using MediatRGen.Languages;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Commands
{
    public static class CommandProcessor
    {

        public static BaseProcess HandleProcess(string process)
        {
            string[] commandArgs = process.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Validator.ValidateCommandBeforeProcess(commandArgs);

            if (string.IsNullOrWhiteSpace(process))
            {
                throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }

            switch (commandArgs[0].ToLower())
            {
                case "create-solution":
                    return new SolutionCreateProcess(process);

                case "create-repository":
                    return new RepositoryProcess(process);

                default:
                    throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }
        }

    }
}
