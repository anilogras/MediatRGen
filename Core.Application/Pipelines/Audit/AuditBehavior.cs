using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Audit
{
    public class AuditBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IAudit
    {

        //todo : Loglayacak sistemi yaz


        //private readonly ICurrentUserService _currentUser;
        //private readonly IAuditLogger _auditLogger;

        //public AuditBehavior(ICurrentUserService currentUser, IAuditLogger auditLogger)
        //{
        //    _currentUser = currentUser;
        //    _auditLogger = auditLogger;
        //}


        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            //var auditEntry = new AuditLog
            //{
            //    UserId = _currentUser.UserId,
            //    RequestName = typeof(TRequest).Name,
            //    RequestData = JsonSerializer.Serialize(request),
            //    Timestamp = DateTime.UtcNow
            //};

            //await _auditLogger.LogAsync(auditEntry); // veritabanına/log'a yaz

            return response;
        }
    }
}
