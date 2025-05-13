using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models.Logger;
using Core.CrossCuttingConcerns.Logger;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

namespace Core.Application.Pipelines.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoggerBase _logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            List<LogParameter> _logParameter = new()
            {
                new LogParameter() {
                    Name = request.GetType().Name,
                    Value = request
                }
            };

            LogDetail _detail = new LogDetail
            {
                FullName = request.GetType().FullName,
                MethodName = request.GetType().Name,
                Parameters = _logParameter,
                User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?"
            };

            _logger.Info(_detail.AsJSON());

            return await next();
        }
    }
}
