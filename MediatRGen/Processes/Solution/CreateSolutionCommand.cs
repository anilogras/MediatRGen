using MediatRGen.Cli.Models;
using MediatRGen.Cli.States;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Processes.Solution
{
    internal class CreateSolutionCommand : Command<CreateSolutionSchema>
    {

        public override int Execute(CommandContext context, CreateSolutionSchema settings)
        {
            GetParameters();
            GetPathFromCommand();

            DirectoryServices.CreateIsNotExist(DirectoryServices.GetCurrentDirectory().Value + _parameter.ProjectName);
            string _directory = _parameter.Directory;

            string _combinedPath = DirectoryServices.GetPath(_directory, _parameter.ProjectName).Value;


            if (FileService.CheckFile(_combinedPath, _parameter.ProjectName + ".sln").Value == true)
            {
                throw new FileException(LangHandler.Definitions().ProjectExist);
            }

            string commandResult = @$"dotnet new sln -n {_parameter.ProjectName} --output ""{_combinedPath}""";

            string res = SystemProcessService.InvokeCommand(commandResult).Value;

            Console.WriteLine(res);

            Console.WriteLine($"cd ./{_parameter.ProjectName}");

            if (res.IndexOf("Error") == -1)
                Console.WriteLine(LangHandler.Definitions().YouCanWriteCode);

            GlobalState.Instance.ProjectName = _parameter.ProjectName;
            GlobalState.Instance.SolutionName = _parameter.ProjectName;

            CreateFirstConfigFile(_combinedPath, GlobalState.Instance);
            return 0; throw new NotImplementedException();
        }


        private static void CreateFirstConfigFile(string _combinedPath, object firstConfig)
        {
            FileService.Create(_combinedPath, GlobalState.ConfigFileName, firstConfig);
        }

        private void GetPathFromCommand()
        {
            string _directory = string.Empty;

            if (_parameter.Directory == "." || string.IsNullOrEmpty(_parameter.Directory))
            {
                _directory = DirectoryServices.GetCurrentDirectory().Value;
            }
            else
                _directory = _parameter.Directory;

            _parameter.Directory = _directory;
        }


    }
}
