using MediatRGen.Cli.Processe2s.Core;
using MediatRGen.Cli.Processe2s.Nuget;
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
        public override int Execute(CommandContext context)
        {
            GlobalState.Instance.UseGateway = false;

            if (GlobalState.Instance == null)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            //if (GlobalState.Instance.Version != null)
            //{
            //    throw new FileException(LangHandler.Definitions().ConfigExist);
            //}

            GlobalState.Instance.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();
            GatewayActive();

            FileService.UpdateConfig(GlobalState.ConfigFileName, GlobalState.Instance);

            CreateCoreFiles();

            Console.WriteLine(LangHandler.Definitions().CreatedConfigFile);
        }

        private void ModuleSystemActive()
        {
            GlobalState.Instance.UseModule = QuestionService.YesNoQuestion(LangHandler.Definitions().ModuleActive).Value;
        }

        private void GatewayActive()
        {
            if (GlobalState.Instance.UseModule == true)
            {
                Console.WriteLine(LangHandler.Definitions().UseOchelot);
                GlobalState.Instance.UseGateway = QuestionService.YesNoQuestion(LangHandler.Definitions().GatewayActive).Value;
            }
        }

        private void CreateCoreFiles()
        {
            new CoreCreateProcess();
            Console.WriteLine(LangHandler.Definitions().CoreFilesCreated);


            new CreateNugetPackages();
            Console.WriteLine(LangHandler.Definitions().NugetPackagesCreated);

        }
    }
}
