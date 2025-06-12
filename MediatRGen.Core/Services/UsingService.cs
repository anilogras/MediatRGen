using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Services
{
    internal static class UsingService
    {

        public static ServiceResult<bool> AddUsing(string classPath, string usingName)
        {
            try
            {
                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = ClassService.GetClassRoot(classPath).Value;

                SyntaxNode newRoot = AddUsing(root, usingName).Value;

                ClassService.ReWriteClass(classPath, newRoot);

                return new ServiceResult<bool>(true, true, "");
                //return new ServiceResult<bool>(true, true, usingName + "\n" + LangHandler.Definitions().UsingAdded);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<SyntaxNode> AddUsing(SyntaxNode root, string usingName)
        {

            var parsedRoot = root as CompilationUnitSyntax;

            if (parsedRoot.Usings.Any(u => u.Name.ToString() == usingName))
                return new ServiceResult<SyntaxNode>(root, true, "");


            if (usingName.IndexOf("\\src\\") != -1)
                usingName = usingName.Substring(usingName.IndexOf("\\src\\") + 5);

            if (usingName.IndexOf("\\") != -1)
                usingName = usingName.Substring(usingName.IndexOf("\\") + 1);

            usingName = usingName.Replace("\\", ".");

            var newUsing = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(usingName))
                                         .WithTrailingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed);

            if (parsedRoot.Usings.ToString().Contains(usingName))
                return new ServiceResult<SyntaxNode>(root, true, "");

            var newRoot = parsedRoot.AddUsings(newUsing);

            var convertedRoot = newRoot as SyntaxNode;

            return new ServiceResult<SyntaxNode>(convertedRoot, true, "");

        }
    }
}