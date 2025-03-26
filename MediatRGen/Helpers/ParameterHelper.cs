using CommandLine;
using MediatRGen.Exceptions;
using MediatRGen.Languages;
using MediatRGen.Processes.Parameters.Solution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
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
    }
}
