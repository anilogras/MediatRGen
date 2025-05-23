using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.MediatRBase.Commads.Delete
{
    public class BaseDeleteCommand<TResponse> : IRequest<TResponse>
        where TResponse : class , new()
    {
    }
}
