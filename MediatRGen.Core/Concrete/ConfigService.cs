using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class ConfigService : IConfigService
    {

        private readonly ISettings _settings;
        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;
        private readonly IClassLibraryService _classLibraryService;
        private readonly ISystemProcessService _systemProcessService;
        private readonly INugetService _nugetServices;
        private readonly ICoreServices _coreServices;
        private readonly IOutputService _outputService;

        public ConfigService(
            ISettings settings,
            IFileService fileService,
            IQuestionService questionService,
            IClassLibraryService classLibraryService,
            ISystemProcessService systemProcessService,
            INugetService nugetServices,
            ICoreServices coreServices,
            IOutputService outputService)
        {
            _settings = settings;
            _fileService = fileService;
            _questionService = questionService;
            _classLibraryService = classLibraryService;
            _systemProcessService = systemProcessService;
            _nugetServices = nugetServices;
            _coreServices = coreServices;
            _outputService = outputService;
        }

        public void Create()
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

            _settings.Update();

            CreateCoreFiles();

            _outputService.Info(LangHandler.Definitions().CreatedConfigFile);
        }

        private void ModuleSystemActive()
        {
            _settings.UseModule = _questionService.YesNoQuestion(LangHandler.Definitions().ModuleActive).Value;
        }

        private void GatewayActive()
        {
            if (_settings.UseModule == true)
            {
                bool res = _questionService.YesNoQuestion(LangHandler.Definitions().UseOchelot).Value;

                if (res == true)
                {
                    _settings.UseGateway = _questionService.YesNoQuestion(LangHandler.Definitions().GatewayActive).Value;
                }
                else
                {
                    _outputService.Info(LangHandler.Definitions().GatewayNotActive);
                }
            }
        }

        private void CreateCoreFiles()
        {
            _coreServices.Create();

            _nugetServices.CreateNugets();
        }

        public void Update()
        {
            Create();
        }
    }
}
