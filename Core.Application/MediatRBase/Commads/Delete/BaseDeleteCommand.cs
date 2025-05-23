using MediatR;

namespace Core.Application.MediatRBase.Commads.Delete
{
    public class BaseDeleteCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
