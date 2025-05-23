using Core.Application.Pipelines.CreateRequestSetProperty;
using MediatR;

namespace Core.Application.MediatRBase.Commads.Create
{
    public class BaseCreateCommand<TResponse> : IRequest<TResponse>, ICreateRequest
        where TResponse : IResponse
    {
    }
}
