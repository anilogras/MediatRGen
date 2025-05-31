using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.States;

namespace MediatRGen.Cli.Processes.Config
{
    public class UpdateConfigProcess : BaseProcess
    {
        public UpdateConfigProcess()
        {
            new CreateConfigProcess();
            GlobalState.UpdateInstance();
        }
    }

}
