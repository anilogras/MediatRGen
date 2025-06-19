using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    internal interface IConstructorService
    {
        public ServiceResult<SyntaxNode> AddConstructor(SyntaxNode root);
        public ServiceResult<SyntaxNode> AddConstructorCode(SyntaxNode root, string code);
        public ServiceResult<SyntaxNode> AddConstructorParameters(SyntaxNode root, string parameters, string baseParameter);

    }
}
