using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IParameterService
    {
        public ServiceResult<T> GetParameter<T>(string command, ref T _parameter)
                    where T : class, new();
        public ServiceResult GetParameterFromConsole(object target, string propertyName, string message);
    }
}
