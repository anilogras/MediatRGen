using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Concrete
{
    internal class InheritanceService : IInheritanceService
    {


        public InheritanceService()
        {
        }

        public ServiceResult<SyntaxNode> SetBaseInheritance(SyntaxNode root, string baseClassName)
        {
            var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            var baseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseClassName));
            var baseList = SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>().Add(baseType));
            var newClassNode = classNode.WithBaseList(baseList);
            var newRoot = root.ReplaceNode(classNode, newClassNode);
            return new ServiceResult<SyntaxNode>(newRoot, true, "");
        }
    }
}