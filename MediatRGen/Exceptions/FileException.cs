using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Exceptions
{
    public class FileException : Exception
    {
        public FileException(string exception) : base(exception)
        {
            
        }
    }
}
