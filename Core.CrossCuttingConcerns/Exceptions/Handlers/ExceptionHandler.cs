using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(Exception exception) =>
              exception switch
              {
                  BusinessException => HandleException(exception),
                  ValidationException => HandleException(exception),
                  ExceptionTypes.TimeoutException => HandleException(exception),
                  TransactionException => HandleException(exception),
                  _ => HandleException(exception)
              };


        protected abstract Task HandleException(BusinessException exception);
        protected abstract Task HandleException(ValidationException exception);
        protected abstract Task HandleException(ExceptionTypes.TimeoutException exception);
        protected abstract Task HandleException(TransactionException exception);
        protected abstract Task HandleException(Exception exception);
    }
}
