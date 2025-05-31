using Core.Application.BaseCQRS;
using Core.Application.Pipelines.CreateRequestSetProperty;
using MediatR;

namespace Core.Application.BaseCQRS.Commands.Create
{
    public class BaseCreateCommand<TResponse> : IRequest<TResponse>, ICreateRequest
        where TResponse : IResponse
    {
        public BaseCreateCommand()
        {
            
        }
    }
}
