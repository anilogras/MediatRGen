using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediatRGen.Core.Concrete
{
    internal class ClassService : IClassService
    {

        private readonly IFileService _fileService;
        private readonly INameSpaceService _nameSpaceService;
        private readonly IDirectoryServices _directoryService;
        private readonly ISystemProcessService _systemProcessService;
        private readonly IInheritanceService _inheritanceService;
        private readonly IUsingService _usingService;
        private readonly IConstructorService _constructorService;
        private readonly IMethodService _methodService;
        private readonly IPropertyService _propertyService;

        public ClassService(
            IFileService fileService,
            INameSpaceService nameSpaceService,
            IDirectoryServices directoryService,
            ISystemProcessService systemProcessService,
            IInheritanceService inheritanceService,
            IUsingService usingService,
            IConstructorService constructorService,
            IMethodService methodService,
            IPropertyService propertyService
            )
        {
            _fileService = fileService;
            _nameSpaceService = nameSpaceService;
            _directoryService = directoryService;
            _systemProcessService = systemProcessService;
            _inheritanceService = inheritanceService;
            _usingService = usingService;
            _constructorService = constructorService;
            _methodService = methodService;
            _propertyService = propertyService;
        }

        public ServiceResult<bool> ReWriteClass(string classPath, SyntaxNode newRoot)
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
        public ServiceResult<SyntaxNode> GetClassRoot(string classPath)
        {
            string? extension = Path.GetExtension(classPath);

            if (string.IsNullOrEmpty(extension))
                classPath = classPath + ".cs";

            string? _classText = _fileService.Get(classPath).Value;

            if (string.IsNullOrEmpty(_classText))
                return new ServiceResult<SyntaxNode>(null, false, LangHandler.Definitions().ClassNotFound);

            var tree = CSharpSyntaxTree.ParseText(_classText);
            var root = tree.GetRoot();
            return new ServiceResult<SyntaxNode>(root, true, "");
        }
        private async Task<ServiceResult<bool>> CreateClass(ClassConfiguration classSettings)
        {
            _directoryService.CreateIsNotExist(classSettings.Directory);
            _systemProcessService.InvokeCommand($"dotnet new class -n {classSettings.Name} -o {classSettings.Directory}");

            var root = GetClassRoot(classSettings.Directory + "\\" + classSettings.Name).Value;
            SyntaxNode _activeNode = _nameSpaceService.ChangeNameSpace(root, classSettings.Directory).Value;

            foreach (var privateField in classSettings.ConstructorPrivateFields)
            {
                _activeNode = _propertyService.AddReadOnlyField(_activeNode, privateField.FieldType, privateField.FieldName, privateField.Accessibility).Value;
            }

            foreach (var attr in classSettings.ClassAttr)
            {
                _activeNode = AddClassAttribute(_activeNode, attr.Name, attr.Value).Value;
            }

            if (!string.IsNullOrEmpty(classSettings.BaseInheritance))
            {
                _activeNode = _inheritanceService.SetBaseInheritance(_activeNode, classSettings.BaseInheritance).Value;
            }

            foreach (var usingText in classSettings.Usings)
            {
                _activeNode = _usingService.AddUsing(_activeNode, usingText).Value;
            }


            if (classSettings.Constructor)
            {
                _activeNode = _constructorService.AddConstructor(_activeNode).Value;
            }

            if (!string.IsNullOrEmpty(classSettings.ConstructorParameters))
            {
                _activeNode = _constructorService.AddConstructorParameters(_activeNode, classSettings.ConstructorParameters, classSettings.ConstructorBaseParameters).Value;
            }

            foreach (var code in classSettings.ConstructorCodes)
            {
                _activeNode = _constructorService.AddConstructorCode(_activeNode, code).Value;
            }

            foreach (var method in classSettings.Methods)
            {
                _activeNode = _methodService.AddMethod(_activeNode, method).Value;
            }

            ReWriteClass(classSettings.Directory + "\\" + classSettings.Name, _activeNode);

            return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassCreated);
        }

        public async Task<ServiceResult<bool>> CreateClass(List<ClassConfiguration> classSettings)
        {

            var tasks = new List<Task>();

            foreach (var _setting in classSettings)
            {
                tasks.Add(Task.Run(() => CreateClass(_setting)));
            }

            await Task.WhenAll(tasks);

            return new ServiceResult<bool>(false, false, "");
        }

        public ServiceResult<SyntaxNode> AddClassAttribute(SyntaxNode root, string attributeName, params string[] arguments)
        {
            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            var attribute = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeName));

            var hasAttribute = classDeclaration.AttributeLists.Select(x => x.ToString()).Any(x => x == $"[{attribute.Name.ToString()}]");
            
            if (hasAttribute)
                return new ServiceResult<SyntaxNode>(root, false, "");


            if (arguments != null)
            {

                var argumentList = SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(
                    arguments.Select(arg =>
                        SyntaxFactory.AttributeArgument(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal(arg)
                            )
                        )
                    )
                );

                attribute = attribute.WithArgumentList(SyntaxFactory.AttributeArgumentList(argumentList));
            }



            var attributeList = SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(attribute));

            var newClass = classDeclaration.AddAttributeLists(attributeList);

            var newRoot = root.ReplaceNode(classDeclaration, newClass);

            return new ServiceResult<SyntaxNode>(newRoot, false, "");
        }
    }
}
