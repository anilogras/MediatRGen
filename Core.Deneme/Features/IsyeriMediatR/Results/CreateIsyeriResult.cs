using Core.Application.BaseCQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Deneme.Features.IsyeriMediatR.Results
{
    public class CreateIsyeriResult : IResponse
    {
        public string Name { get; set; }
    }
}
