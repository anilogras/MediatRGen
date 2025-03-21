using MediatRGen.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen
{
    public static class Validator
    {
        public static void CommandValidator(string[] commandArgs)
        {
            CheckCommand(commandArgs[0]);
            CheckParams(commandArgs);            
        }

        private static void CheckParams(string[] commandArgs)
        {
            if (commandArgs.Length == 0 || !commandArgs[0].StartsWith("-"))
            {
                throw new InvalidParameterException(LangHandler.Definitions().InvalidCommandName);
            }
        }

        private static void CheckCommand(string command)
        {
            string[] activeCommand = ["create"];

            if(!activeCommand.Contains(command))
            {
                throw new InvalidCommandException(LangHandler.Definitions().InvalidCommandName);
            }
        }
    }
}
