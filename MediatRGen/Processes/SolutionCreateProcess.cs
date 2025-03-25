using CommandLine;
using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Processes.Parameters.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class SolutionCreateProcess : BaseProcess
    {
        public SolutionCreateProcess(string process)
        {
            try
            {
                ParserResult<SolutionCreateParameter> _parsedOptions = Parser.Default.ParseArguments<SolutionCreateParameter>(ArgHelper.SplitArgs(process));
                if (_parsedOptions.Errors.Count() != 0) 
                {
                    throw new Exception("parse Exception");
                }

                SolutionCreateParameter opt = _parsedOptions.Value;
            }
            catch (Exception exception)
            {
                throw new ParameterParseException(LangHandler.Definitions().InvalidParamForCreateSolution);
            }

            solutionCreate();
        }

        private void solutionCreate()
        {
            Console.WriteLine("solutionCreated");
        }
    }
}
