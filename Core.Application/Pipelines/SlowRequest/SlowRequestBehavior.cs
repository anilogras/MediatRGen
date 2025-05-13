using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.SlowRequest
{
    public class SlowRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        //Aşağıda belirtilen süreden daha uzun süren istekler yavaş istek olarak kabul edilir.
        //todo : Loglayacak sistemi yaz

        private readonly TimeSpan maxTime = TimeSpan.FromMilliseconds(1000);

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var sw = Stopwatch.StartNew();

            var response = await next();

            sw.Stop();


            if (sw.Elapsed > maxTime)
            {
                //var requestName = typeof(TRequest).Name;
                //_logger.LogWarning(
                //    "Yavaş istek tespit edildi: {RequestName} - Süre: {ElapsedMilliseconds}ms - İçerik: {@Request}",
                //    requestName,
                //    sw.ElapsedMilliseconds,
                //    request);
            }

            return response;

        }
    }
}
