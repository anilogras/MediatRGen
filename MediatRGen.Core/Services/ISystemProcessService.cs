using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface ISystemProcessService
    {
        public ServiceResult<string> InvokeCommand(string parameters, string command = "dotnet");
        public ServiceResult<bool> BuildProject(string projectName);
    }
}
