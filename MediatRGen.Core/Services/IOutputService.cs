using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IOutputService
    {
        public void Info(string message);
        public void Error(string message);
    }
}
