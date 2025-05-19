using MediatRGen.Cli.Languages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Helpers
{
    public static class ClassHelper
    {

        //public ClassHelper()
        //{
        //    //var classString2 = ChangeNameSpace(classString, "NewNameSpace");

        //    //var res = AddNewProperty(classString2, "DenemeProp", SyntaxKind.StringKeyword, false, false);
        //}

        public static string AddNewProperty(string classString, string propertyName, SyntaxKind propertyType, bool getProp = true, bool setProp = true)
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
            return root.NormalizeWhitespace().ToFullString();
        }

        public static void ChangeNameSpace(string classPath, string newNameSpace)
        {

            string? extension = Path.GetExtension(classPath);

            if (string.IsNullOrEmpty(extension))
                classPath = classPath + ".cs";

            string? _classText = FileHelpers.Get(classPath);

            newNameSpace = newNameSpace.Substring(newNameSpace.IndexOf("\\src\\") + 5);
            newNameSpace = newNameSpace.Replace("\\", ".");



            if (string.IsNullOrEmpty(_classText))
                throw new FileNotFoundException(LangHandler.Definitions().ClassNotFound);

            var tree = CSharpSyntaxTree.ParseText(_classText);
            var root = tree.GetRoot();


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

                var updatedCode = newRoot.NormalizeWhitespace().ToFullString();


                File.WriteAllText(classPath, updatedCode);
            }
            else
            {
                Console.WriteLine(LangHandler.Definitions().NameSpaceNotFound);
            }
        }

    }
}
