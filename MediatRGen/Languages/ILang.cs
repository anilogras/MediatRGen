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
        public string FileExist { get;}

        public string YouCanWriteCode { get;}
    }
}
