using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using MediatRGen.Processes.Base;
using MediatRGen.Processes.Parameters.Module;
using MediatRGen.Processes.Parameters.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Module
{
    public class CreateModuleProcess : BaseProcess
    {

        private readonly ProjectModule _modul;
        private ModuleCreateParameter _parameter;

        public CreateModuleProcess(string command)
        {

            ParameterHelper.GetParameter<ModuleCreateParameter>(command, ref _parameter);

            _modul = new ProjectModule();


            if (string.IsNullOrEmpty(_parameter.Name))
            {
                GetModuleName();
            }

            Configuration _configuration = FileHelpers.GetConfig();

            CheckModulNameIsExist(_configuration);

            _configuration.Modules.Add(_modul);



            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), _modul.Name);


            //DirectoryHelpers.CreateIsNotExist(PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory() , _modul.Name))


            FileHelpers.UpdateConfig(_configuration);
        }

        private void CheckModulNameIsExist(Configuration? _configuration)
        {
            if (_configuration.Modules == null)
                _configuration.Modules = new List<ProjectModule>();

            if (_configuration.Modules.Where(x => x.Name == _modul.Name).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }

        private void GetModuleName()
        {
            object answer = string.Empty;
            QuestionHelper.GetAnswer(LangHandler.Definitions().ModuleName, ref answer);

            _modul.Name = answer?.ToString();
        }
    }
}
