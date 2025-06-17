using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Schemas
{
    public class CreateSolutionBaseSchema
    {
        public string ProjectName { get; set; }
        public string Directory { get; set; }
    }

}
