using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.Base
{
    public class ServiceResult<T>
    {

        public ServiceResult(T _value, bool status)
        {
            Value = _value;
            Status = status;
        }


        public T Value { get; set; }
        public bool Status { get; set; }
        public List<ServiceError> Errors { get; set; }
    }

    public class ServiceError
    {
        public string Error { get; set; }
    }

}
