using CommandLine;
using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.Processes.Parameters.Solution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Helpers
{
    public static class ParameterHelper
    {

        public static T GetParameter<T>(string command, ref T _parameter)
        where T : class, new()
        {
            try
            {
                ParserResult<T> _parsedOptions = Parser.Default.ParseArguments<T>(ArgHelpers.SplitArgs(command));
                if (_parsedOptions.Errors.Count() != 0)
                {
                    throw new Exception("parse Exception");
                }

                _parameter = _parsedOptions.Value;

            }
            catch (Exception exception)
            {
                throw new ParameterParseException(LangHandler.Definitions().InvalidParamForCreateSolution);
            }

            return _parameter;
        }

        public static void GetParameterFromConsole(object target, string propertyName, string message)
        {
            var prop = target.GetType().GetProperty(propertyName);

            if (prop == null || !prop.CanWrite) return;

            var currentValue = prop.GetValue(target) as string;

            if (string.IsNullOrWhiteSpace(currentValue))
            {
                Console.WriteLine(message);

                while (true)
                {
                    string value = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        prop.SetValue(target, value);
                        break;
                    }
                }
            }
        }


    }
}
