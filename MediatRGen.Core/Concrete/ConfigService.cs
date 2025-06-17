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

        public ConfigService(
            ISettings settings,
            IFileService fileService,
            IQuestionService questionService,
            IClassLibraryService classLibraryService,
            ISystemProcessService systemProcessService,
            INugetService nugetServices,
            ICoreServices coreServices)
        {
            _settings = settings;
            _fileService = fileService;
            _questionService = questionService;
            _classLibraryService = classLibraryService;
            _systemProcessService = systemProcessService;
            _nugetServices = nugetServices;
            _coreServices = coreServices;
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

            _fileService.UpdateConfig(_settings.ConfigFileName, _settings.Get());

            CreateCoreFiles();

            Console.WriteLine(LangHandler.Definitions().CreatedConfigFile);
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

        public void Update()
        {
            Create();
        }
    }
}
