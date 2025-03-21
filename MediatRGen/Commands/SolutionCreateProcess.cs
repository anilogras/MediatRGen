using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Commands
{
    public class SolutionCreateProcess : BaseProcess
    {
        public SolutionCreateProcess(string process)
        {
            solutionCreate();
        }

        private void solutionCreate()
        {
            Console.WriteLine("solutionCreate");
        }
    }
}
