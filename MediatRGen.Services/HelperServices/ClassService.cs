using MediatRGen.Core.Exceptions;
using MediatRGen.Services.Base;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Services.HelperServices
{
    public class ClassService
    {
        public static ServiceResult<string> AddNewProperty(string classString, string propertyName, SyntaxKind propertyType, bool getProp = true, bool setProp = true)
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
        public static ServiceResult<bool> ChangeNameSpace(string classPath, string newNameSpace)
        {
            try
            {
                SyntaxNode root = GetClassRoot(classPath).Value;
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

                    ReWriteClass(classPath, newRoot);
                    return new ServiceResult<bool>(true, true, "");
                    //return new ServiceResult<bool>(true, true, LangHandler.Definitions().NameSpaceChanged);
                }
                else
                {
                    return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceNotFound);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<bool> AddConstructor(string classPath )
        {
            try
            {
                SyntaxNode root = GetClassRoot(classPath).Value;

                var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
                if (classNode == null)
                    return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassNotFound);

                var ctor = SyntaxFactory.ConstructorDeclaration(classNode.Identifier.Text)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .WithBody(SyntaxFactory.Block());
                
                var newClassNode = classNode.AddMembers(ctor);

                var newRoot = root.ReplaceNode(classNode, newClassNode);

                ReWriteClass(classPath, newRoot);

                return new ServiceResult<bool>(false, false, "");

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<string> GetNameSpace(string classPath)
        {
            try
            {

                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = GetClassRoot(classPath).Value;

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
        public static ServiceResult<bool> AddUsing(string classPath, string usingName)
        {
            try
            {
                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                SyntaxNode root = GetClassRoot(classPath).Value;
                var parsedRoot = root as CompilationUnitSyntax;

                if (parsedRoot.Usings.Any(u => u.Name.ToString() == usingName))
                    return new ServiceResult<bool>(true, true, "");


                if (usingName.IndexOf("\\src\\") != -1)
                    usingName = usingName.Substring(usingName.IndexOf("\\src\\") + 5);

                if (usingName.IndexOf("\\") != -1)
                    usingName = usingName.Substring(usingName.IndexOf("\\") + 1);

                usingName = usingName.Replace("\\", ".");

                var newUsing = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(usingName))
                                             .WithTrailingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed);

                var newRoot = parsedRoot.AddUsings(newUsing);

                var convertedRoot = newRoot as SyntaxNode;

                ReWriteClass(classPath, newRoot);

                return new ServiceResult<bool>(true, true, "");
                //return new ServiceResult<bool>(true, true, usingName + "\n" + LangHandler.Definitions().UsingAdded);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().NameSpaceChangeException, new ClassLibraryException(ex.Message));
            }
        }
        public static ServiceResult<bool> SetBaseInheritance(string classPath, string baseClassName)
        {
            try
            {
                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";


                SyntaxNode root = GetClassRoot(classPath).Value;
                var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                var baseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseClassName));
                var baseList = SyntaxFactory.BaseList(SyntaxFactory.SeparatedList<BaseTypeSyntax>().Add(baseType));
                var newClassNode = classNode.WithBaseList(baseList);
                var newRoot = root.ReplaceNode(classNode, newClassNode);
                ReWriteClass(classPath, newRoot);

                // return new ServiceResult<bool>(true, true, LangHandler.Definitions().BaseClassSet);
                return new ServiceResult<bool>(true, true, "");
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().BaseClassSetError, new ClassLibraryException(ex.Message));
            }
        }
        private static ServiceResult<bool> ReWriteClass(string classPath, SyntaxNode newRoot)
        {
            try
            {

                string? extension = Path.GetExtension(classPath);

                if (string.IsNullOrEmpty(extension))
                    classPath = classPath + ".cs";

                var updatedCode = newRoot.NormalizeWhitespace().ToFullString();
                File.WriteAllText(classPath, updatedCode);
                //return new ServiceResult<bool>(true, true, LangHandler.Definitions().ReWriteClass, null);
                return new ServiceResult<bool>(true, true, "", null);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ReWriteClassError, new ClassLibraryException(ex.Message));
            }

        }
        private static ServiceResult<SyntaxNode> GetClassRoot(string classPath)
        {
            string? extension = Path.GetExtension(classPath);

            if (string.IsNullOrEmpty(extension))
                classPath = classPath + ".cs";

            string? _classText = FileService.Get(classPath).Value;

            if (string.IsNullOrEmpty(_classText))
                return new ServiceResult<SyntaxNode>(null, false, LangHandler.Definitions().ClassNotFound);

            var tree = CSharpSyntaxTree.ParseText(_classText);
            var root = tree.GetRoot();
            return new ServiceResult<SyntaxNode>(root, true, "");
        }
    }
}
