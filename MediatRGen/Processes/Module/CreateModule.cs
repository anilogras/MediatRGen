using MediatRGen.Cli.Helpers;
using MediatRGen.Cli.Languages;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Module;
using MediatRGen.Cli.States;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Module
{
    public class CreateModule : BaseProcess
    {
        private ModuleCreateParameter _parameter;



        public CreateModule(string command)
        {
            ParameterHelper.GetParameter<ModuleCreateParameter>(command, ref _parameter);

            ParameterHelper.GetParameterFromConsole(_parameter, "ModuleName", LangHandler.Definitions().EnterModuleName);

            Execute();
        }

        private void Execute()
        {

            CheckModulNameIsExist();
            DirectoryHelpers.CreateIsNotExist("./src", _parameter.ModuleName);

            ClassLibraryHelpers.Create(_parameter.ModuleName + "." + "Domain", DirectoryHelpers.GetPath(_parameter.ModuleName));
            ClassLibraryHelpers.Create(_parameter.ModuleName + "." + "Application", DirectoryHelpers.GetPath(_parameter.ModuleName));
            ClassLibraryHelpers.Create(_parameter.ModuleName + "." + "Infrastructure", DirectoryHelpers.GetPath(_parameter.ModuleName));
            WebApiHelper.Create(_parameter.ModuleName + "." + "API", DirectoryHelpers.GetPath(_parameter.ModuleName));

            GlobalState.Instance.Modules.Add(new ProjectModule()
            {
                Name = _parameter.ModuleName
            });

            FileHelpers.UpdateConfig();

        }

        private void CheckModulNameIsExist()
        {
            if (GlobalState.Instance.Modules.Where(x => x.Name == _parameter.ModuleName).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }
    }
}
