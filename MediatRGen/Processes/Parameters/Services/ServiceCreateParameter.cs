using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Parameters.Services
{
    public class ServiceCreateParameter
    {
        [Option('e', "entity", Required = false)]
        public string EntityName { get; set; }


        [Option('m', "module", Required = false)]
        public string ModuleName { get; set; }

    }
}
