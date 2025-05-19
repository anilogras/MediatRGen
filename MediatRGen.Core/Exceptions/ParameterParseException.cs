using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions
{
    public class ParameterParseException : Exception
    {
        public ParameterParseException(string exception) : base(exception)
        {

        }
    }
}
