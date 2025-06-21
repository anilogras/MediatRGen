using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IMethodService
    {
        public ServiceResult<SyntaxNode> AddMethod(SyntaxNode root, string code);
    }
}
