using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Concrete
{
    internal class NameSpaceService : INameSpaceService
    {
        public NameSpaceService()
        {
        }

        public ServiceResult<SyntaxNode> ChangeNameSpace(SyntaxNode root, string newNameSpace)
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

        public ServiceResult<string> GetNameSpace(SyntaxNode root)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>("", true, LangHandler.Definitions().NameSpaceNotFound, new ClassLibraryException(ex.Message));
            }
        }
    }
}