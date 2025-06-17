using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface ICoreServices
    {
        public ServiceResult<bool> Create();
    }
}
