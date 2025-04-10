using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.Processes.Base;
using MediatRGen.Processes.Core;
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
        private readonly string? _projectName;
        private readonly string? _path;
        private Configuration? _configuration;

        public CreateConfigProcess()
        {
            _configuration = FileHelpers.GetConfig();
            _configuration.UseGateway = false;

            if (_configuration == null)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            if (_configuration.Version != null)
            {
                throw new FileException(LangHandler.Definitions().ConfigExist);
            }

            _configuration.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();
            GatewayActive();

            FileHelpers.UpdateConfig(_configuration);

            CreateCoreFiles();
        }

        private void ModuleSystemActive()
        {
            _configuration.UseModule = QuestionHelper.YesNoQuestion(LangHandler.Definitions().ModuleActive);
        }

        private void GatewayActive()
        {
            if (_configuration.UseModule == true)
            {
                Console.WriteLine(LangHandler.Definitions().UseOchelot);
                _configuration.UseGateway = QuestionHelper.YesNoQuestion(LangHandler.Definitions().GatewayActive);

            }
        }

        private void CreateCoreFiles()
        {
            new CoreCreateProcess();
            Console.WriteLine(LangHandler.Definitions().CoreFilesCreated);
        }
    }
}
