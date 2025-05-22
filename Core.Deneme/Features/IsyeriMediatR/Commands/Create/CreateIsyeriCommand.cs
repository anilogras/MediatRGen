using Core.Deneme.Features.IsyeriMediatR.Results;
using Core.Persistence.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Commands.Create
{
    public class CreateIsyeriCommand : IRequest<CreateIsyeriResult>
    {
        public string Name { get; set; }
    }
}
