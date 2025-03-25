using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Processes.Parameters.Solution
{
    public class SolutionCreateParameter
    {
        [Option('d', "dir", Required = false, HelpText = "Dizin yolu.")]
        public string Directory { get; set; }
    }
}
