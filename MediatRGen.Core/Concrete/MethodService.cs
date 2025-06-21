using MediatRGen.Core.Base;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete
{
    internal class MethodService : IMethodService
    {
        public ServiceResult<SyntaxNode> AddMethod(SyntaxNode root, string code)
        {
            var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            var existingMethodNames = classNode.Members
                .OfType<MethodDeclarationSyntax>()
                .Select(m => m.Identifier.Text);

            var method = SyntaxFactory.ParseMemberDeclaration(code);

            if (method is MethodDeclarationSyntax methodSyntax &&
                !existingMethodNames.Contains(methodSyntax.Identifier.Text))
            {
                var newClass = classNode.AddMembers(method);
                root = root.ReplaceNode(classNode, newClass);
            }
            return new ServiceResult<SyntaxNode>(root, true, "");
        }
    }
}
