using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {


        private HttpResponse? _response;
        public HttpResponse Response
        {
            get => _response ?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }

        protected override Task HandleException(BusinessException businessException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJSON();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(ValidationException validationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new ValidationProblemDetails(validationException.Errors).AsJSON();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new ExceptionProblemDetails(exception.Message).AsJSON();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(ExceptionTypes.TimeoutException exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new ExceptionProblemDetails(exception.Message).AsJSON();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(TransactionException exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new ExceptionProblemDetails(exception.Message).AsJSON();
            return Response.WriteAsync(details);
        }
    }
}
