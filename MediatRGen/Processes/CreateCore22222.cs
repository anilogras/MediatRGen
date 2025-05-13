using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes
{
    public class CreateCore22222
    {
        public CreateCore22222()
        {
            string classString = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Models
{
    public class Config
    {

        public Config()
        {
            Modules = new List<ProjectModule>();
        }

        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
";
            var classString2 = ChangeNameSpace(classString, "NewNameSpace");

            var res = AddNewProperty(classString2, "DenemeProp", SyntaxKind.StringKeyword, false, false);


        }

        public string AddNewProperty(string classString, string propertyName, SyntaxKind propertyType, bool getProp = true, bool setProp = true)
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

        public string ChangeNameSpace(string classString, string newNameSpace)
        {
            var createCs = CSharpSyntaxTree.ParseText(classString);
            var root = createCs.GetRoot();  // Root'a erişim
            // Tüm namespace'leri alıyoruz
            var namespaceDeclaration = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            var newNamespaceName = SyntaxFactory.IdentifierName(newNameSpace);
            var updatedNamespaceDeclaration = namespaceDeclaration.WithName(newNamespaceName);
            root = root.ReplaceNode(namespaceDeclaration, updatedNamespaceDeclaration);
            return root.NormalizeWhitespace().ToFullString();
        }

    }
}
