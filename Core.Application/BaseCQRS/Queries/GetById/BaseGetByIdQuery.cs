using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Queries.GetById
{
    public class GetByIdBaseQuery<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : class, IEntity, new()
    {
        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
