using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Core;
using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Config
{
    public class UpdateConfigProcess : BaseProcess
    {
        public UpdateConfigProcess()
        {
            new CoreCreateProcess();
            GlobalState.UpdateInstance();
        }
    }

}
