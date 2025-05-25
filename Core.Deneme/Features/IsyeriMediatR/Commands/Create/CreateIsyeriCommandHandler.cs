using AutoMapper;
using Core.Application.BaseCQRS.Commands.Create;
using Core.Deneme.Features.IsyeriMediatR.Results;
using Core.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Commands.Create
{
    public class CreateIsyeriCommandHandler : BaseCreateCommandHandler<CreateIsyeriCommand, CreateIsyeriResult, Isyeri>
    {
        public CreateIsyeriCommandHandler(IRepository<Isyeri> repository , IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
