using MediatRGen.Cli.States;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Services;

namespace MediatRGen.Cli.Processes.Core
{
    public class CoreCreateProcess
    {
        private readonly IClassLibraryService _classLibraryService;
        private readonly ISystemProcessService _systemProcessService;
        public CoreCreateProcess(IClassLibraryService classLibraryService, ISystemProcessService systemProcessService)
        {
            _classLibraryService = classLibraryService;
            _systemProcessService = systemProcessService;
        }

        public CoreCreateProcess()
        {
            _classLibraryService.Create("Core.Persistence", "Core", GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            _systemProcessService.BuildProject(GlobalState.Instance.ProjectName);
        }

    }
}
