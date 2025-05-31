using Core.Application.BaseCQRS;
using MediatR;

namespace Core.Application.BaseCQRS.Commands.Update
{
    public class BaseUpdateCommand<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}
