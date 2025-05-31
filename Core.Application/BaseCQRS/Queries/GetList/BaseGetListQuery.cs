using AutoMapper;
using Core.Persistence.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.BaseCQRS.Queries.GetList
{
    public class BaseGetListQuery<TResponse> : IRequest<TResponse>
    {

    }
}
