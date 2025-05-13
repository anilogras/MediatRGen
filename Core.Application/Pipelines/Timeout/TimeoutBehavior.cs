using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Timeout
{
    public class TimeoutBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(1500);

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cts = new CancellationTokenSource();

            var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;

            var task = Task.Run(async () =>
            {
                return await next();
            }, combinedToken);

            if (await Task.WhenAny(task, Task.Delay(_timeout, combinedToken)) == task)
            {
                return await task;
            }
            else
            {
                //todo : loglacak sistemi yaz
                throw new CrossCuttingConcerns.Exceptions.ExceptionTypes.TimeoutException($"Request processing exceeded the time limit of {_timeout.TotalSeconds} seconds.");
            }
        }
    }
}
