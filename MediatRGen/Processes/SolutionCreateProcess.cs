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

        private readonly SolutionCreateParameter _parameter;

        public SolutionCreateProcess(string process)
        {
            try
            {
                ParserResult<SolutionCreateParameter> _parsedOptions = Parser.Default.ParseArguments<SolutionCreateParameter>(ArgHelpers.SplitArgs(process));
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

            solutionCreate();
        }

        private void solutionCreate()
        {
            string _directory = string.Empty;

            if (_parameter.Directory == ".")
            {
                _directory = DirectoryHelpers.GetAppDirectory();
            }else
                _directory = _parameter.Directory;

            DirectoryHelpers.CreateIsNotExist(_directory, _parameter.ProjectName);
            string _combinetPath = PathHelper.GetPath(_directory , _parameter.ProjectName);

            string commandResult = @$"dotnet new sln -n {_parameter.ProjectName} --output {_combinetPath}";

            if (FileHelpers.CheckFile(_combinetPath  , _parameter.ProjectName+".sln") == true) 
            {
                throw new FileException(LangHandler.Definitions().FileExist);
            }

            string res = SystemProcessHelpers.InvokeCommand(commandResult);
            Console.WriteLine(res);
        }
    }
}
