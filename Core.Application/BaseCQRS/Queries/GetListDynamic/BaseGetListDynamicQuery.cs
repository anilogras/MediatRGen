using Core.Persistence.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.BaseCQRS.Queries.GetListDynamic
{
    public class BaseGetListDynamicQuery<TResponse> : IRequest<TResponse>
    {

    }
}
