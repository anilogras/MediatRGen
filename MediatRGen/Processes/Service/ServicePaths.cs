using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Service
{
    public class ServicePaths
    {
        public string DomainPath { get; set; } 
        public string EntityPath { get; set; }
        public string EntityName { get; set; }
        public string EntityDirectory { get; set; }
        public string EntityLocalDirectory { get; set; }
        public string ApplicationModulePath { get; set; }
        public string EntityNameNotExt { get; set; }
        public string EntityPathNotExt { get; set; }
        public string EntityPluralName { get; set; }
    }
}
