using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

        public ClassService(
            IFileService fileService,
            INameSpaceService nameSpaceService,
            IDirectoryServices directoryService,
            ISystemProcessService systemProcessService,
            IInheritanceService inheritanceService,
            IUsingService usingService,
            IConstructorService constructorService)
        {
            _fileService = fileService;
            _nameSpaceService = nameSpaceService;
            _directoryService = directoryService;
            _systemProcessService = systemProcessService;
            _inheritanceService = inheritanceService;
            _usingService = usingService;
            _constructorService = constructorService;
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
        public ServiceResult<bool> CreateClass(ClassConfiguration classSettings)
        {
            _directoryService.CreateIsNotExist(classSettings.Directory);
            _systemProcessService.InvokeCommand($"dotnet new class -n {classSettings.Name} -o {classSettings.Directory}");

            var root = GetClassRoot(classSettings.Directory + "\\" + classSettings.Name).Value;
            SyntaxNode _activeNode = _nameSpaceService.ChangeNameSpace(root, classSettings.Directory).Value;

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




            ReWriteClass(classSettings.Directory + "\\" + classSettings.Name, _activeNode);

            return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassCreated);
        }
    }
}
