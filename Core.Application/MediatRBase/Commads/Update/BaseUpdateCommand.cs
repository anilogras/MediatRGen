using MediatR;

namespace Core.Application.MediatRBase.Commads.Update
{
    public class BaseUpdateCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
