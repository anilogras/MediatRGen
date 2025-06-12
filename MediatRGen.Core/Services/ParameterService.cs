using CommandLine;
using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using System;

namespace MediatRGen.Core.Services
{
    public class ParameterService
    {

        public static ServiceResult<T> GetParameter<T>(string command, ref T _parameter)
        where T : class, new()
        {
            try
            {
                ParserResult<T> _parsedOptions = Parser.Default.ParseArguments<T>(ArgsService.SplitArgs(command).Value);
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

            return new ServiceResult<T>(_parameter, true, "");
        }

        public static ServiceResult GetParameterFromConsole(object target, string propertyName, string message)
        {
            try
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
                return new ServiceResult(false, "");
                //return new ServiceResult(false, LangHandler.Definitions().PropertySetted);
            }
            catch (Exception ex)
            {

                return new ServiceResult(false, LangHandler.Definitions().PropertyNotFountOrWeritable, ex);

            }
        }


    }
}
