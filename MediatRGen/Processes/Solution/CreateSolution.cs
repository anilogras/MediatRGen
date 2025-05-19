using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatRGen.Cli.Processes.Parameters.Solutions;
using MediatRGen.Core.Exceptions;


namespace MediatRGen.Cli.Processes.Solution
{
    public class CreateSolutionProcess : BaseProcess
    {

        private readonly SolutionCreateParameter _parameter;


        public CreateSolutionProcess(string command)
        {
            ParameterHelper.GetParameter<SolutionCreateParameter>(command, ref _parameter);
            GetParameters();
            GetPathFromCommand();
            Execute();
        }

        private void GetParameters()
        {
            ParameterHelper.GetParameterFromConsole(_parameter, "ProjectName", LangHandler.Definitions().EnterProjectName);
        }

        private void Execute()
        {

            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), _parameter.ProjectName);
            string _directory = _parameter.Directory;

            string _combinedPath = DirectoryHelpers.GetPath(_directory, _parameter.ProjectName);


            if (FileHelpers.CheckFile(_combinedPath, _parameter.ProjectName + ".sln") == true)
            {
                throw new FileException(LangHandler.Definitions().ProjectExist);
            }

            string commandResult = @$"dotnet new sln -n {_parameter.ProjectName} --output ""{_combinedPath}""";

            string res = SystemProcessHelpers.InvokeCommand(commandResult);
            Console.WriteLine(res);
            Console.WriteLine($"cd ./{_parameter.ProjectName}");
            if (res.IndexOf("Error") == -1)
                Console.WriteLine(LangHandler.Definitions().YouCanWriteCode);

            GlobalState.Instance.ProjectName = _parameter.ProjectName;
            GlobalState.Instance.SolutionName = _parameter.ProjectName;

            CreateFirstConfigFile(_combinedPath, GlobalState.Instance);
        }


        private static void CreateFirstConfigFile(string _combinedPath, object firstConfig)
        {
            FileHelpers.Create(_combinedPath, GlobalState.ConfigFileName, firstConfig);
        }

        private void GetPathFromCommand()
        {
            string _directory = string.Empty;

            if (_parameter.Directory == "." || string.IsNullOrEmpty(_parameter.Directory))
            {
                _directory = DirectoryHelpers.GetCurrentDirectory();
            }
            else
                _directory = _parameter.Directory;

            _parameter.Directory = _directory;
        }


    }
}
