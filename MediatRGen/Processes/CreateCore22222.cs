using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes
{
    public class CreateCore22222
    {
        public CreateCore22222()
        {
            string classString = @"using System;
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
        public List<ProjectModule> Modules { get; set; }
    }
}
";
  


        }

     

    }
}
