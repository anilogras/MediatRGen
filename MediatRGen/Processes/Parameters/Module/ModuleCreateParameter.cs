using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Parameters.Module
{
    public class ModuleCreateParameter
    {

        [Option('n', "name", Required = false)]
        public string ModuleName { get; set; }
    }
}
