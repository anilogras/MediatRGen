using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatRGen.Processes.Base;

namespace MediatRGen.Processes
{
    public class RepositoryProcess : BaseProcess
    {
        public RepositoryProcess(string process)
        {
            repositoryCreate();
        }

        private void repositoryCreate()
        {
            Console.WriteLine("repositoryCreate");
        }
    }
}
