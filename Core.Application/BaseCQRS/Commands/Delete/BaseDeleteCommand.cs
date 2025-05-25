using MediatR;

namespace Core.Application.MediatRBase.Commands.Delete
{
    public class BaseDeleteCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
