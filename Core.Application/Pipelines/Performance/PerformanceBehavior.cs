using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Performance
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IPerformance
    {

        //todo : Loglayacak sistemi yaz

        private readonly Stopwatch _timer = new Stopwatch();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();
            //Console.WriteLine($"[PERF] {typeof(TRequest).Name} took {_timer.ElapsedMilliseconds} ms");
            return response;
        }
    }
}
