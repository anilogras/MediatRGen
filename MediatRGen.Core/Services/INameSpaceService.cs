using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface INameSpaceService
    {
        public ServiceResult<bool> ChangeNameSpace(string classPath, string newNameSpace);
        public ServiceResult<SyntaxNode> ChangeNameSpace(SyntaxNode root, string newNameSpace);
        public ServiceResult<string> GetNameSpace(string classPath);

    }
}
