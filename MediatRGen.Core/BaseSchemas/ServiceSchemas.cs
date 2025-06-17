using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Schemas
{
    public record CreateModuleBaseSchema(string ModuleName);
    public record CreateServiceBaseSchema(string EntityName, string ModuleName);
    public record CreateSolutionBaseSchema(string ProjectName , string Directory);

}
