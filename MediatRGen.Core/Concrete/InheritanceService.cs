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

        private readonly IClassService _classService;

        public InheritanceService(IClassService classService)
        {
            _classService = classService;
        }

        public ServiceResult<bool> SetBaseInheritance(string classPath, string baseClassName)
        {
            try
            {
                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = _classService.GetClassRoot(classPath).Value;
                SyntaxNode newRoot = SetBaseInheritance(root, baseClassName).Value;
                _classService.ReWriteClass(classPath, newRoot);
                return new ServiceResult<bool>(true, true, "");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().BaseClassSetError, new ClassLibraryException(ex.Message));
            }
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