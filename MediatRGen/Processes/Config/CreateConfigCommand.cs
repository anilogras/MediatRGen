using MediatRGen.Cli.Processes.Core;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Cli.States;
using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Config
{
    public class CreateConfigCommand : Command
    {
        private readonly ISettings _setting;
        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;

        public CreateConfigCommand(ISettings setting, IFileService fileService, IQuestionService questionService)
        {
            _setting = setting;
            _fileService = fileService;
            _questionService = questionService;
        }

        public override int Execute(CommandContext context)
        {
            _setting.UseGateway = false;

            if (_setting == null)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            //if (_setting.Version != null)
            //{
            //    throw new FileException(LangHandler.Definitions().ConfigExist);
            //}

            _setting.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();
            GatewayActive();

            _fileService.UpdateConfig(GlobalState.ConfigFileName, _setting.Get());

            CreateCoreFiles();

            Console.WriteLine(LangHandler.Definitions().CreatedConfigFile);

            return 0;
        }

        private void ModuleSystemActive()
        {
            _setting.UseModule = _questionService.YesNoQuestion(LangHandler.Definitions().ModuleActive).Value;
        }

        private void GatewayActive()
        {
            if (_setting.UseModule == true)
            {
                Console.WriteLine(LangHandler.Definitions().UseOchelot);
                _setting.UseGateway = _questionService.YesNoQuestion(LangHandler.Definitions().GatewayActive).Value;
            }
        }

        private void CreateCoreFiles()
        {
            new CreateCoreCommand();
            Console.WriteLine(LangHandler.Definitions().CoreFilesCreated);


            new CreateNugetCommand();
            Console.WriteLine(LangHandler.Definitions().NugetPackagesCreated);

        }
    }
}
