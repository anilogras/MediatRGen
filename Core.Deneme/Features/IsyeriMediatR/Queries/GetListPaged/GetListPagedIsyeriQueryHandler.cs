using AutoMapper;
using Core.Application.BaseCQRS.Queries.GetListPaged;
using Core.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Queries.GetListPaged
{
    public class GetListPagedIsyeriQueryHandler : BaseGetListPagedQueryHandler<GetListPagedIsyeriQuery, Isyeri, Isyeri>
    {
        public GetListPagedIsyeriQueryHandler(IRepository<Isyeri> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
