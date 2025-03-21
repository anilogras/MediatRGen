using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Language
{
    public interface ILang
    {
        public string InvalidCommandName { get; }
        public string InvalidParameter { get;}
    }
}
