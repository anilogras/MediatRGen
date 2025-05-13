using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _exceptionHandler;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _exceptionHandler = new HttpExceptionHandler();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/problem+json";
            _exceptionHandler.Response = response;
            return _exceptionHandler.HandleExceptionAsync(exception);
        }

    }
}
