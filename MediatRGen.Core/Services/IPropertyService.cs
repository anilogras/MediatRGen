using MediatRGen.Core.Base;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IPropertyService
    {
        public ServiceResult<SyntaxNode> AddReadOnlyField(SyntaxNode root, string fieldType, string fieldName, SyntaxKind accessibility = SyntaxKind.PrivateKeyword);
        public ServiceResult<string> AddNewProperty(string classString, string propertyName, SyntaxKind propertyType, bool getProp = true, bool setProp = true);
    }
}
