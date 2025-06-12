using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Core;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Cli.States;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;

namespace MediatRGen.Cli.Processes.Config
{
    public class CreateConfigProcess : BaseProcess
    {
        public CreateConfigProcess()
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
