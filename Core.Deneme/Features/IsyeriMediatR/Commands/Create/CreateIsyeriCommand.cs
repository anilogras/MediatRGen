using Core.Deneme.Features.IsyeriMediatR.Results;
using Core.Persistence.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.MediatRBase.Commands.Create;

namespace Core.Deneme.Features.IsyeriMediatR.Commands.Create
{
    public class CreateIsyeriCommand : BaseCreateCommand<CreateIsyeriResult>
    {
        public string Name { get; set; }
    }
}
