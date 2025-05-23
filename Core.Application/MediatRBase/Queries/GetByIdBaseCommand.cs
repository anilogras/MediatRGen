using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.MediatRBase.Queries
{
    public class GetByIdBaseCommand<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : class, new()
    {
        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
