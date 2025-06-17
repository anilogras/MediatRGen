using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IClassLibraryService
    {
        public ServiceResult<bool> Create(string name, string path, string projectName, string solutionName);
    }
}
