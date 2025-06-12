using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IFileService
    {
        public ServiceResult<bool> Create(string path, string fileName, string content);
        public ServiceResult<bool> Create(string path, string fileName, object content);
        public ServiceResult<bool> Create(string path, string fileName);
        public ServiceResult<string?> Get(string path);
        public ServiceResult<bool> UpdateConfig(string configFileName, object stateInstance);
        public ServiceResult<string> FindFileRecursive(string directory, string targetFile);
        public ServiceResult<bool> CheckFile(string path, string fileName);
    }
}
