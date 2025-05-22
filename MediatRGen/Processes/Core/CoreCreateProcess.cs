using MediatRGen.Cli.States;
using MediatRGen.Services.HelperServices;

namespace MediatRGen.Cli.Processes.Core
{
    public class CoreCreateProcess
    {
        public CoreCreateProcess()
        {
            ClassLibraryService.Create("Core.Persistence", "Core", GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            SystemProcessService.BuildProject(GlobalState.Instance.ProjectName);
        }

    }
}
