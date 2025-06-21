using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Models
{
    public class ClassConfiguration
    {

        public ClassConfiguration()
        {
            Usings = new List<string>();
            ConstructorCodes = new List<string>();
            Methods = new List<string>();
            ConstructorPrivateFields = new List<PropertyConfiguration>();
            ClassAttr = new List<ClassAttributeConfiguration>();
        }

        public string Directory { get; set; }
        public string Name { get; set; }
        public string BaseInheritance { get; set; }
        public List<string> Usings { get; set; }
        public bool Constructor { get; set; }
        public string ConstructorParameters { get; set; }
        public List<PropertyConfiguration> ConstructorPrivateFields { get; set; }
        public List<ClassAttributeConfiguration> ClassAttr { get; set; }
        public string ConstructorBaseParameters { get; set; }
        public List<string> ConstructorCodes { get; set; }
        public List<string> Methods { get; set; }
    }


    public class PropertyConfiguration
    {

        public string FieldType { get; set; }
        public string FieldName { get; set; }
        public SyntaxKind Accessibility { get; set; }
    }

    public class ClassAttributeConfiguration
    {
        public string Name { get; set; }
        public string[] Value { get; set; }
    }

}
