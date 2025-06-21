using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete
{
    internal class CoreServices : ICoreServices
    {

        private readonly IClassLibraryService _classLibraryService;
        private readonly ISystemProcessService _systemProcessService;
        private readonly ISettings _settings;
        private readonly IOutputService _outputService;

        public CoreServices(
            IClassLibraryService classLibraryService,
            ISystemProcessService systemProcessService,
            ISettings settings,
            IOutputService outputService)
        {
            _classLibraryService = classLibraryService;
            _systemProcessService = systemProcessService;
            _settings = settings;
            _outputService = outputService;
        }

        public ServiceResult<bool> Create()
        {
            _classLibraryService.Create("Core.Persistence", "Core", _settings.ProjectName, _settings.SolutionName);
            _systemProcessService.BuildProject(_settings.ProjectName);
            _outputService.Info(LangHandler.Definitions().CoreFilesCreated);
            return new ServiceResult<bool>(true, true, "");
        }
    }
}
