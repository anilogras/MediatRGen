using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Processes.Solution
{
    public class CreateSolutionCommand : Command<CreateSolutionBaseSchema>
    {
        private CreateSolutionBaseSchema _parameter;

        private readonly IDirectoryServices _directoryService;
        private readonly ISettings _settings;
        private readonly ISystemProcessService _systemProcessService;
        private readonly IFileService _fileService;
        private readonly IParameterService _parameterService;

        public CreateSolutionCommand(
            IDirectoryServices directoryService,
            ISettings settings,
            ISystemProcessService systemProcessService,
            IFileService fileService,
            IParameterService parameterService)
        {
            _directoryService = directoryService;
            _settings = settings;
            _systemProcessService = systemProcessService;
            _fileService = fileService;
            _parameterService = parameterService;
        }

        public override int Execute(CommandContext context, CreateSolutionBaseSchema settings)
        {
            _parameter = settings;

            _parameterService.GetParameterFromConsole(_parameter, "ProjectName", LangHandler.Definitions().EnterProjectName);

            GetPathFromCommand();

            _directoryService.CreateIsNotExist(_directoryService.GetCurrentDirectory().Value + settings.ProjectName);
            string _directory = settings.Directory;

            string _combinedPath = _directoryService.GetPath(_directory, settings.ProjectName).Value;


            if (_fileService.CheckFile(_combinedPath, settings.ProjectName + ".sln").Value == true)
            {
                throw new FileException(LangHandler.Definitions().ProjectExist);
            }

            string commandResult = @$"dotnet new sln -n {settings.ProjectName} --output ""{_combinedPath}""";

            string res = _systemProcessService.InvokeCommand(commandResult).Value;

            Console.WriteLine(res);

            Console.WriteLine($"cd ./{settings.ProjectName}");

            if (res.IndexOf("Error") == -1)
                Console.WriteLine(LangHandler.Definitions().YouCanWriteCode);

            _settings.ProjectName = settings.ProjectName;
            _settings.SolutionName = settings.ProjectName;

            CreateFirstConfigFile(_combinedPath, _settings.Get());
            return 0;
        }


        private void CreateFirstConfigFile(string _combinedPath, object firstConfig)
        {
            _fileService.Create(_combinedPath, _settings.ConfigFileName, firstConfig);
        }

        private void GetPathFromCommand()
        {
            string _directory = string.Empty;

            if (_parameter.Directory == "." || string.IsNullOrEmpty(_parameter.Directory))
            {
                _directory = _directoryService.GetCurrentDirectory().Value;
            }
            else
                _directory = _parameter.Directory;

            _parameter.Directory = _directory;
        }


    }
}
