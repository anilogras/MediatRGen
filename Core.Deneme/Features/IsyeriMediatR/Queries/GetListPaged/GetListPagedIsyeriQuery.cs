using Core.Application.BaseCQRS.Queries.GetListPaged;
using Core.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Queries.GetListPaged
{
    public class GetListPagedIsyeriQuery : BaseGetListPagedQuery<Isyeri>, IPageRequest
    {
        public PageRequest PageRequest { get; set; }
    }
}
