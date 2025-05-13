using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Exceptions
{
    public class ModuleException : Exception
    {
        public ModuleException(string exception) : base(exception)
        {

        }
    }
}
