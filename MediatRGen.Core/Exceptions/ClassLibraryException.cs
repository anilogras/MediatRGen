using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions
{
    public class ClassLibraryException : Exception
    {
        public ClassLibraryException(string message) : base(message)
        {
        }
    }
}
