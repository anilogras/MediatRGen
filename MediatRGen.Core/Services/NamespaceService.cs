using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Services
{
    public static class NameSpaceService
    {
        public static ServiceResult<bool> ChangeNameSpace(string classPath, string newNameSpace)
        {
            try
            {
                SyntaxNode root = ClassService.GetClassRoot(classPath).Value;
                var newRoot = ChangeNameSpace(root, newNameSpace).Value;
                ClassService.ReWriteClass(classPath, newRoot);
                return new ServiceResult<bool>(true, true, "");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<SyntaxNode> ChangeNameSpace(SyntaxNode root, string newNameSpace)
        {
            newNameSpace = newNameSpace.Substring(newNameSpace.IndexOf("\\src\\") + 5);
            newNameSpace = newNameSpace.Substring(newNameSpace.IndexOf("\\") + 1);
            newNameSpace = newNameSpace.Replace("\\", ".");


            var fileScopedNs = root.DescendantNodes()
                .OfType<FileScopedNamespaceDeclarationSyntax>()
                .FirstOrDefault();

            var blockNs = root.DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>()
                .FirstOrDefault();

            var namespaceNode = (BaseNamespaceDeclarationSyntax?)fileScopedNs ?? blockNs;

            if (namespaceNode != null)
            {
                var newNamespace = SyntaxFactory.ParseName(newNameSpace);
                var newNamespaceNode = namespaceNode.WithName(newNamespace);
                var newRoot = root.ReplaceNode(namespaceNode, newNamespaceNode);
                return new ServiceResult<SyntaxNode>(newRoot, true, "");
                //return new ServiceResult<bool>(true, true, LangHandler.Definitions().NameSpaceChanged);
            }
            else
            {
                return new ServiceResult<SyntaxNode>(null, false, LangHandler.Definitions().NameSpaceNotFound, new ClassLibraryException(LangHandler.Definitions().NameSpaceNotFound));
            }
        }

        public static ServiceResult<string> GetNameSpace(string classPath)
        {
            try
            {

                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = ClassService.GetClassRoot(classPath).Value;

                var fileScopedNs = root.DescendantNodes()
                   .OfType<FileScopedNamespaceDeclarationSyntax>()
                   .FirstOrDefault();

                var blockNs = root.DescendantNodes()
                    .OfType<NamespaceDeclarationSyntax>()
                    .FirstOrDefault();

                var namespaceNode = (BaseNamespaceDeclarationSyntax?)fileScopedNs ?? blockNs;

                if (string.IsNullOrEmpty(namespaceNode?.Name.ToString()))
                    return new ServiceResult<string>("", false, "", new ClassLibraryException(LangHandler.Definitions().NameSpaceNotFound));
                else
                    return new ServiceResult<string>(namespaceNode.Name.ToString(), true, "");

                //return new ServiceResult<string>(namespaceNode.Name.ToString(), true, LangHandler.Definitions().NameSpaceFound);
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>("", true, LangHandler.Definitions().NameSpaceNotFound, new ClassLibraryException(ex.Message));
            }
        }
    }
}