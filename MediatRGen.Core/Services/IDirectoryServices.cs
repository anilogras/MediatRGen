using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    internal interface IDirectoryServices
    {
        public ServiceResult<string> GetPath(params string[] paths);
        public ServiceResult<string> ClearTwiceBackSlash(string path);
        public ServiceResult<string> GetCurrentDirectory();
        public ServiceResult<bool> CreateIsNotExist(string path);
        public ServiceResult<bool> Delete(string path);
    }
}
