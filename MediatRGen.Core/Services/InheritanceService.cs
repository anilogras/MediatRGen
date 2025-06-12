using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Services
{
    public static class InheritanceService
    {

        public static ServiceResult<bool> SetBaseInheritance(string classPath, string baseClassName)
        {
            try
            {
                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = ClassService.GetClassRoot(classPath).Value;
                SyntaxNode newRoot = SetBaseInheritance(root, baseClassName).Value;
                ClassService.ReWriteClass(classPath, newRoot);
                return new ServiceResult<bool>(true, true, "");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().BaseClassSetError, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<SyntaxNode> SetBaseInheritance(SyntaxNode root, string baseClassName)
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