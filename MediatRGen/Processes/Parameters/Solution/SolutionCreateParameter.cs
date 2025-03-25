using CommandLine;
using MediatRGen.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Parameters.Solution
{
    public class SolutionCreateParameter
    {
        [Option('d', "dir", Required = false)]
        public string Directory { get; set; }

        [Option('n', "name", Required = true)]
        public string ProjectName { get; set; }
    }
}
