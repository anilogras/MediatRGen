using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IInheritanceService
    {
        public ServiceResult<bool> SetBaseInheritance(string classPath, string baseClassName);
        public ServiceResult<SyntaxNode> SetBaseInheritance(SyntaxNode root, string baseClassName);
    }
}
