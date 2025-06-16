using MediatRGen.Cli.Processes.Core;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Config
{
    public class CreateConfigCommand : Command
    {
        private readonly ISettings _settings;
        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;
        private readonly IClassLibraryService _classLibraryService;
        private readonly ISystemProcessService _systemProcessService;
        private readonly INugetService _nugetServices;
        private readonly ICoreServices _coreServices;

        public CreateConfigCommand(ISettings settings, IFileService fileService, IQuestionService questionService, ISystemProcessService systemProcessService, IClassLibraryService classLibraryService, INugetService nugetServices , ICoreServices coreServices)
        {
            _settings = settings;
            _fileService = fileService;
            _questionService = questionService;
            _systemProcessService = systemProcessService;
            _classLibraryService = classLibraryService;
            _nugetServices = nugetServices;
            _coreServices = coreServices;
        }

        public override int Execute(CommandContext context)
        {
            _settings.UseGateway = false;

            if (_settings == null)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            //if (_setting.Version != null)
            //{
            //    throw new FileException(LangHandler.Definitions().ConfigExist);
            //}

            _settings.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();
            GatewayActive();

            _fileService.UpdateConfig(_settings.ConfigFileName, _settings.Get());

            CreateCoreFiles();

            Console.WriteLine(LangHandler.Definitions().CreatedConfigFile);

            return 0;
        }

        private void ModuleSystemActive()
        {
            _settings.UseModule = _questionService.YesNoQuestion(LangHandler.Definitions().ModuleActive).Value;
        }

        private void GatewayActive()
        {
            if (_settings.UseModule == true)
            {
                Console.WriteLine(LangHandler.Definitions().UseOchelot);
                _settings.UseGateway = _questionService.YesNoQuestion(LangHandler.Definitions().GatewayActive).Value;
            }
        }

        private void CreateCoreFiles()
        {
            _coreServices.Create();
            Console.WriteLine(LangHandler.Definitions().CoreFilesCreated);

            _nugetServices.CreateNugets();
            Console.WriteLine(LangHandler.Definitions().NugetPackagesCreated);
        }
    }
}
