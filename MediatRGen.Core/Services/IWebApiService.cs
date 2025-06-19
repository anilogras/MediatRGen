using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IWebApiService
    {
        public ServiceResult<bool> Create(string name, string path);
        public ServiceResult<bool> AddController(string name, string path);
    }
}
