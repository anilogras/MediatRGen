using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions
{
    public class SystemProcessException : Exception
    {
        public SystemProcessException(string exception) : base(exception)
        {

        }
    }
}
