using MediatRGen.Cli.States;
using MediatRGen.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli
{
    public static class Validator 
    {
        public static void ValidateCommandBeforeProcess(string[] commandArgs)
        {
            CheckCommand(commandArgs[0]);
            CheckParams(commandArgs);
        }

        private static void CheckParams(string[] commandArgs)
        {
            if (commandArgs.Length != 1 && commandArgs.Length == 0 && !commandArgs[0].StartsWith("-"))
            {
                throw new InvalidParameterException(LangHandler.Definitions().InvalidCommandName);
            }
        }

        private static void CheckCommand(string command)
        {
            string[] activeCommand = GlobalState.Instance.Commands;

            if (!activeCommand.Contains(command))
            {
                throw new InvalidCommandException { Way = "DENEME AAAAAAA", Message = LangHandler.Definitions().InvalidCommandName };
            }
        }
    }
}
