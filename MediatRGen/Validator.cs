using MediatRGen.Core;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli
{
    public class Validator : IValidateService
    {

        private readonly ISettings _settings;

        public Validator(ISettings settings)
        {
            _settings = settings;
        }

        public void ValidateCommandBeforeProcess(string[] commandArgs)
        {
            CheckCommand(commandArgs[0]);
            CheckParams(commandArgs);
        }

        private void CheckParams(string[] commandArgs)
        {
            if (commandArgs.Length != 1 && commandArgs.Length == 0 && !commandArgs[0].StartsWith("-"))
            {
                throw new InvalidParameterException(LangHandler.Definitions().InvalidCommandName);
            }
        }

        private void CheckCommand(string command)
        {
            string[] activeCommand = _settings.Commands;

            if (!activeCommand.Contains(command))
            {
                throw new InvalidCommandException { Way = "DENEME AAAAAAA", Message = LangHandler.Definitions().InvalidCommandName };
            }
        }
    }
}
