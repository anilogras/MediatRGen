using MediatR;

namespace Core.Application.BaseCQRS.Commands.Delete
{
    public class BaseDeleteCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
