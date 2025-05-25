using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Queries.GetList
{
    public class BaseGetListQuery<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : class, IEntity, new()
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
