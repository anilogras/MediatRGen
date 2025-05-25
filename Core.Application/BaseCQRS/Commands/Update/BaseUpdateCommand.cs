using MediatR;

namespace Core.Application.MediatRBase.Commands.Update
{
    public class BaseUpdateCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
