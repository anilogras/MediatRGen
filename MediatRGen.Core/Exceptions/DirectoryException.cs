using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions
{
    public class DirectoryException : Exception
    {
        public DirectoryException(string exception) : base(exception)
        {

        }
    }
}
