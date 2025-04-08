using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Models
{
    public class Config
    {

        public Config()
        {
            Modules = new List<ProjectModule>();
        }

        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
