using Core.Application.Requests;
using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Queries.GetListPaged
{
    public class BaseGetListPagedQuery<TResponse> : IRequest<TResponse>, IPageRequest
    {
        public PageRequest PageRequest { get; set; }
    }
}
