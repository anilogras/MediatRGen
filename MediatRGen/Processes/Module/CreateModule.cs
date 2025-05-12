using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.Processes.Base;
using MediatRGen.Processes.Parameters.Module;
using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Module
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

            ClassLibraryHelpers.Create(ClassLibraryHelpers.CreateClassLibraryName(_parameter.ModuleName), DirectoryHelpers.GetCurrentDirectory());

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
