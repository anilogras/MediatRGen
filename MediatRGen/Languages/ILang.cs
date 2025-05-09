using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Languages
{
    public interface ILang
    {
        public string InvalidCommandName { get; }
        public string InvalidParameter { get; }
        public string InvalidParamForCreateSolution { get; }
        public string EnterCommand { get;}
        public string ExistDirectory { get;}
        public string ProjectExist { get;}
        public string ConfigExist { get; }
        public string ConfigNotFoundVersionExist { get; }
        public string ConfigNotFound{ get; }

        public string YouCanWriteCode { get;}

        public string CreatedConfigFile { get;}
        public string ModuleActive { get; }
        public string ModuleName{ get; }

        public string ModuleNameIsRequired { get; }
        public string UseOchelot { get; }

        public string Required { get; }

        public string ModuleIsDefined { get; }

        public string EnterProjectName { get; }

    }
}
