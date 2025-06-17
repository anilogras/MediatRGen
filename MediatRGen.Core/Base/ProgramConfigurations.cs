using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Base
{
    internal class ProgramConfigurations
    {
        public ProgramConfigurations()
        {
            Modules = new List<ProjectModule>();
            Lang = "tr";
        }

        public string ProjectName { get; set; }
        public string Lang { get; set; }
        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
