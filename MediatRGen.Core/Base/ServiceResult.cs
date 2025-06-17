using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Base
{
    public class ServiceResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Exception? Error { get; set; }

        public ServiceResult(bool status, string message)
        {
            Status = status;
            Message = message;
            InvokeResult();
        }

        public ServiceResult(bool status, string message, Exception? error)
        {
            Status = status;
            Message = message;
            Error = error;
            InvokeResult();
        }

        private void InvokeResult()
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                if (Error != null)
                {
                    if (Message is not "")
                        outputService.Error(Message);

                    throw Error;
                }
                else
                {
                    if (Message is not "")
                        outputService.Info(Message);
                }
            }
        }
    }

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
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                if (Error != null)
                {
                    if (Message is not "")
                        outputService.Error(Message);

                    throw Error;
                }
                else
                {
                    if (Message is not "")
                        outputService.Info(Message);
                }
            }
        }
    }

}
