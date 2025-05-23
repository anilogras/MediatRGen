using Core.Application.Pipelines.CreateRequestSetProperty;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.MediatRBase.Commads.Create
{
    public class BaseCreateCommand<TResponse> : IRequest<TResponse>, ICreateRequest
        where TResponse : IResponse
    {
    }
}
