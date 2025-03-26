using CommandLine;
using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Processes.Parameters.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class SolutionCreateProcess : BaseProcess
    {

        private readonly SolutionCreateParameter _parameter;

        public SolutionCreateProcess(string process)
        {

            ParameterHelper.GetParameter<SolutionCreateParameter>(process, ref _parameter);
            solutionCreate();
        }

        private void solutionCreate()
        {
            string _directory = GetPathFromCommand();

            DirectoryHelpers.CreateIsNotExist(_directory, _parameter.ProjectName);
            string _combinedPath = PathHelper.GetPath(_directory, _parameter.ProjectName);

            string commandResult = @$"dotnet new sln -n {_parameter.ProjectName} --output ""{_combinedPath}""";

            if (FileHelpers.CheckFile(_combinedPath, _parameter.ProjectName + ".sln") == true)
            {
                throw new FileException(LangHandler.Definitions().ProjectExist);
            }

            string res = SystemProcessHelpers.InvokeCommand(commandResult);
            Console.WriteLine(res);
            Console.WriteLine($"cd {_combinedPath}");
            if (res.IndexOf("Error") == -1)
                Console.WriteLine(LangHandler.Definitions().YouCanWriteCode);

            var firstConfig = new
            {
                SolutionName = _parameter.ProjectName
            };

            CreateFirstConfigFile(_combinedPath, firstConfig);
        }

        private static void CreateFirstConfigFile(string _combinedPath, object firstConfig)
        {
            FileHelpers.Create(_combinedPath, "mediatr-config.cnf", JsonSerializer.Serialize(firstConfig));
        }

        private string GetPathFromCommand()
        {
            string _directory = string.Empty;

            if (_parameter.Directory == "." || string.IsNullOrEmpty(_parameter.Directory))
            {
                _directory = DirectoryHelpers.GetCurrentDirectory();
            }
            else
                _directory = _parameter.Directory;
            return _directory;
        }
    }
}
