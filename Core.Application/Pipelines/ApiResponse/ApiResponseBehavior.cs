using Core.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.ApiResponse
{
    public class ApiResponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var result = await next();

                var wrapped = new ApiResponse<TResponse>
                {
                    Success = true,
                    Data = result
                };

                return (TResponse)(object)wrapped;
            }
            catch (Exception ex)
            {
                var fail = new ApiResponse<TResponse>
                {
                    Success = false,
                    Message = ex.Message
                };

                return (TResponse)(object)fail;
            }
        }
    }
}
