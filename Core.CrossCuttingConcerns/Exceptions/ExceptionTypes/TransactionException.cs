using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.ExceptionTypes
{

    public class TransactionException : Exception
    {
        public TransactionException()
        {

        }

        public TransactionException(string? message) : base(message)
        {

        }

        public TransactionException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }


}
