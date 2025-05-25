using MediatR;

namespace Core.Application.BaseCQRS.Commands.Update
{
    public class BaseUpdateCommandHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : class, new()
    {
        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
