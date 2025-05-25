using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Pagination;
using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Queries.GetListPaged
{
    public class BaseGetListPagedQueryHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse> , IPageRequest
        where TEntity : class, IEntity, new()
    {

        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseGetListPagedQueryHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Paging<TEntity> models = await _repository.GetListPagedAsync(
                            index: request.PageRequest.PageIndex,
                            size: request.PageRequest.PageSize
                         );

            var response = _mapper.Map<TResponse>(models);

            return response;
        }
    }
}