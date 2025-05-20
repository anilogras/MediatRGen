using MediatRGen.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.Base
{
    public class ServiceResult<T>
    {

        public ServiceResult(T _value, bool status, string message)
        {
            Value = _value;
            Status = status;
            Message = message;
            InvokeResult();
        }

        public ServiceResult(T _value, bool status, string message, Exception? error)
        {
            Value = _value;
            Status = status;
            Message = message;
            Error = error;
            InvokeResult();
        }

        public T Value { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public Exception? Error { get; set; }

        private void InvokeResult()
        {
            if (Error != null)
            {
                if (Message is not "")
                    Console.Write(Message);

                throw Error;

            }
            else
                Console.Write(Message);
        }

    }

}
