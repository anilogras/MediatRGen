using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Queries.GetById
{
    public class BaseGetByIdQuery<TResponse> : IRequest<TResponse>
    {

    }
}
