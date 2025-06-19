using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IUsingService
    {
        public ServiceResult<SyntaxNode> AddUsing(SyntaxNode root, string usingName);
    }
}
