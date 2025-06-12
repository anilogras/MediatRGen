using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Concrete
{
    internal class PropertyService : IPropertyService
    {
        public ServiceResult<string> AddNewProperty(string classString, string propertyName, SyntaxKind propertyType, bool getProp = true, bool setProp = true)
        {
            try
            {
                var createCs = CSharpSyntaxTree.ParseText(classString);
                var root = createCs.GetRoot();
                var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                var accessors = new List<AccessorDeclarationSyntax>();

                var newProperty = SyntaxFactory.PropertyDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(propertyType)), propertyName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .NormalizeWhitespace();

                if (getProp == true)
                {
                    accessors.Add(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        );
                }

                if (setProp == true)
                {
                    accessors.Add(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        );
                }

                if (accessors.ToArray().Length > 0)
                    newProperty = newProperty.AddAccessorListAccessors(accessors.ToArray());
                else
                    newProperty = newProperty.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

                var updatedClassDeclaration = classDeclaration.AddMembers(newProperty);

                // Root'u güncelliyoruz
                root = root.ReplaceNode(classDeclaration, updatedClassDeclaration);

                return new ServiceResult<string>(root.NormalizeWhitespace().ToFullString(), true, LangHandler.Definitions().ClassCreated);
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>(null, false, LangHandler.Definitions().ClassCreateError, new ClassLibraryException(ex.Message));
            }
        }
    }
}