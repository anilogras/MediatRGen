using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions.FileExceptions
{
    public class FileDeleteException : Exception
    {
        public FileDeleteException(string message) : base(message)
        {
        }
    }
}
