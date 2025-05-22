using CommandLine;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Helpers;
using MediatRGen.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.HelperServices
{
    public class ParameterService
    {

        public static ServiceResult<T> GetParameter<T>(string command, ref T _parameter)
        where T : class, new()
        {
            try
            {
                ParserResult<T> _parsedOptions = Parser.Default.ParseArguments<T>(ArgHelpers.SplitArgs(command));
                if (_parsedOptions.Errors.Count() != 0)
                {
                    return new ServiceResult<T>(null, false, LangHandler.Definitions().ParameterParseError);
                }
                _parameter = _parsedOptions.Value;
            }
            catch (Exception exception)
            {
                return new ServiceResult<T>(null, false, LangHandler.Definitions().InvalidParamForCreateSolution, exception);
            }

            return new ServiceResult<T>(_parameter, true, LangHandler.Definitions().ParameterParsed);
        }

        public static ServiceResult GetParameterFromConsole(object target, string propertyName, string message)
        {
            var prop = target.GetType().GetProperty(propertyName);

            if (prop == null || !prop.CanWrite) return new ServiceResult(false, LangHandler.Definitions().PropertyNotFountOrWeritable);

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
            return new ServiceResult(false, LangHandler.Definitions().PropertySetted);
        }


    }
}
