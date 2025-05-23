using AutoMapper;
using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.MediatRBase.Commads.Delete
{
    public class BaseDeleteCommandHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
        where TEntity : class, IEntity, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseDeleteCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TEntity entity = _mapper.Map<TEntity>(request);
            var result = await _repository.DeleteAsync(entity);
            return _mapper.Map<TResponse>(result);
        }
    }
}
