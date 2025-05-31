using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests
{
    public interface IPageRequest
    {
        public PageRequest PageRequest { get; set; }
    }
}
