using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Services.Models
{
    public class ClassConfiguration
    {

        public ClassConfiguration()
        {
            Usings = new List<string>();
        }

        public string Directory { get; set; }
        public string Name { get; set; }
        public string BaseInheritance { get; set; }
        public List<string> Usings { get; set; }
        public bool Constructor { get; set; }
        public string ConstructorParameters { get; set; }

    }
}
