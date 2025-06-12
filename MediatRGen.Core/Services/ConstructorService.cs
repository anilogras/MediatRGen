using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Services
{
    public static class ConstructorService
    {
        public static ServiceResult<bool> AddConstructor(string classPath)
        {
            try
            {
                SyntaxNode root = ClassService.GetClassRoot(classPath).Value;

                var newRoot = AddConstructor(root).Value;

                ClassService.ReWriteClass(classPath, newRoot);

                return new ServiceResult<bool>(false, false, "");

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<SyntaxNode> AddConstructor(SyntaxNode root)
        {
            var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();

            if (classNode == null)
                return new ServiceResult<SyntaxNode>(null, false, LangHandler.Definitions().ClassNotFound, new ClassLibraryException(LangHandler.Definitions().ClassNotFound));

            bool hasDefaultCtor = classNode.Members.OfType<ConstructorDeclarationSyntax>()
                              .Any();

            SyntaxNode newRoot = root;

            if (!hasDefaultCtor)
            {
                var ctor = SyntaxFactory.ConstructorDeclaration(" " + classNode.Identifier.Text)
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                        .WithBody(SyntaxFactory.Block());

                var newClassNode = classNode.AddMembers(ctor);

                newRoot = root.ReplaceNode(classNode, newClassNode);
            }

            return new ServiceResult<SyntaxNode>(newRoot, true, "");
        }
        public static ServiceResult<SyntaxNode> AddConstructorCode(SyntaxNode root, string code)
        {

            var ctor = root.DescendantNodes()
                    .OfType<ConstructorDeclarationSyntax>()
                    .FirstOrDefault(c => c.ParameterList.Parameters.Count == 0);


            var row = SyntaxFactory.ParseStatement(code);

            bool alreadyExists = ctor.Body.Statements.ToString().Replace(" ", "").Contains(row.ToFullString().Trim().Replace(" ", ""));

            if (alreadyExists)
                return new ServiceResult<SyntaxNode>(root, true, "");

            var newCtorBody = ctor.Body.AddStatements(row);

            var newCtor = ctor.WithBody(newCtorBody);

            var newRoot = root.ReplaceNode(ctor, newCtor);

            return new ServiceResult<SyntaxNode>(newRoot, true, "");
        }
        public static ServiceResult<SyntaxNode> AddConstructorParameters(SyntaxNode root, string parameters, string baseParameter)
        {

            var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();


            var ctor = root.DescendantNodes()
                    .OfType<ConstructorDeclarationSyntax>()
                    .FirstOrDefault();

            var ctorCode = ctor.ToFullString();

            if (ctorCode.Replace(" ", "").IndexOf(parameters.Replace(" ", "")) != -1)
            {
                return new ServiceResult<SyntaxNode>(root, true, "");
            }

            var insertIndex = ctorCode.IndexOf(')');

            if (!string.IsNullOrEmpty(baseParameter))
                parameters = $"{parameters}):base({baseParameter}";

            ctorCode = ctorCode.Insert(insertIndex, parameters);

            var newCtor = SyntaxFactory.ParseMemberDeclaration(ctorCode) as ConstructorDeclarationSyntax;


            var newRoot = root.ReplaceNode(ctor, newCtor);

            return new ServiceResult<SyntaxNode>(newRoot, true, "");
        }
    }
}