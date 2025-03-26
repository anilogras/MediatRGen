using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class ModuleCreateProcess : BaseProcess
    {

        private ProjectModule _modul;

        public ModuleCreateProcess()
        {
            _modul = new ProjectModule();

            GetModuleName();

            Config? _config = FileHelpers.GetConfig();

            if (_config.Modules == null)
                _config.Modules = new List<ProjectModule>();

            if (_config.Modules.Where(x => x.Name == _modul.Name).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }


            _config.Modules.Add(_modul);



            DirectoryHelpers.CreateIsNotExist(DirectoryHelpers.GetCurrentDirectory(), _modul.Name);

            FileHelpers.UpdateConfig(_config);
        }

        private void GetModuleName()
        {
            object answer = string.Empty;
            QuestionHelper.GetAnswer(LangHandler.Definitions().ModuleName, ref answer);

            _modul.Name = answer?.ToString();
        }
    }
}
