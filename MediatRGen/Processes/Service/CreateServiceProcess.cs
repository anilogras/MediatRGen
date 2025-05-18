using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Module;
using MediatRGen.Cli.Processes.Parameters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Service
{
    public class CreateServiceProcess : BaseProcess
    {

        private ServiceCreateParameter _parameter;

        public CreateServiceProcess(string command)
        {
            ParameterHelper.GetParameter<ServiceCreateParameter>(command, ref _parameter);
            ParameterHelper.GetParameterFromConsole(_parameter, "EntityName", LangHandler.Definitions().EnterEntityName);
            ParameterHelper.GetParameterFromConsole(_parameter, "ModuleName", LangHandler.Definitions().EnterModuleName);

            Execute();
        }

        private void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
