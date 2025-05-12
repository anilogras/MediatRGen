using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.Processes.Base;
using MediatRGen.Processes.Core;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Config
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

            if (GlobalState.Instance.Version != null)
            {
                throw new FileException(LangHandler.Definitions().ConfigExist);
            }

            GlobalState.Instance.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();
            GatewayActive();

            FileHelpers.UpdateConfig();

            CreateCoreFiles();
        }

        private void ModuleSystemActive()
        {
            GlobalState.Instance.UseModule = QuestionHelper.YesNoQuestion(LangHandler.Definitions().ModuleActive);
        }

        private void GatewayActive()
        {
            if (GlobalState.Instance.UseModule == true)
            {
                Console.WriteLine(LangHandler.Definitions().UseOchelot);
                GlobalState.Instance.UseGateway = QuestionHelper.YesNoQuestion(LangHandler.Definitions().GatewayActive);
            }
        }

        private void CreateCoreFiles()
        {
            new CoreCreateProcess();
            Console.WriteLine(LangHandler.Definitions().CoreFilesCreated);
        }
    }
}
