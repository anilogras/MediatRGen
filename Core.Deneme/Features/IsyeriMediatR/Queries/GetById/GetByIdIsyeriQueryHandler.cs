using AutoMapper;
using Core.Application.BaseCQRS.Queries.GetById;
using Core.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Queries.GetById
{
    public class GetByIdIsyeriQueryHandler : BaseGetByIdQueryHandler<GetByIdIsyeriQuery, Isyeri, Isyeri>
    {
        public GetByIdIsyeriQueryHandler(IRepository<Isyeri> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
