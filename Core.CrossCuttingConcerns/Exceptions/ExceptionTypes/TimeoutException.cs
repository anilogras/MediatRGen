using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.ExceptionTypes
{
    public class TimeoutException : Exception
    {
        public TimeoutException()
        {

        }

        public TimeoutException(string? message) : base(message)
        {

        }

        public TimeoutException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
