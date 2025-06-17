using CommandLine;
using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System;

namespace MediatRGen.Core.Concrete
{
    internal class ParameterService : IParameterService
    {

        private readonly IArgsService _argsService;
        private readonly IOutputService _outputService;

        public ParameterService(IArgsService argsService, IOutputService outputService)
        {
            _argsService = argsService;
            _outputService = outputService;
        }

        public ServiceResult<T> GetParameter<T>(string command, ref T _parameter)

        where T : class, new()
        {
            try
            {
                ParserResult<T> _parsedOptions = Parser.Default.ParseArguments<T>(_argsService.SplitArgs(command).Value);
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
        public ServiceResult GetParameterFromConsole(object target, string propertyName, string message)
        {

            //todo readline ile data alıyor , bunu düzelt 

            try
            {
                var prop = target.GetType().GetProperty(propertyName);

                if (prop == null || !prop.CanWrite) return new ServiceResult(false, LangHandler.Definitions().PropertyNotFountOrWeritable);

                var currentValue = prop.GetValue(target) as string;

                if (string.IsNullOrWhiteSpace(currentValue))
                {
                    _outputService.Info(message);

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
