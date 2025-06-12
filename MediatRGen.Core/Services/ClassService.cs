using MediatRGen.Core.Base;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace MediatRGen.Core.Services
{
    public class ClassService
    {


        public static ServiceResult<bool> ReWriteClass(string classPath, SyntaxNode newRoot)
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
        public static ServiceResult<SyntaxNode> GetClassRoot(string classPath)
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

        public static ServiceResult<bool> CreateClass(ClassConfiguration classSettings)
        {
            DirectoryServices.CreateIsNotExist(classSettings.Directory);
            SystemProcessService.InvokeCommand($"dotnet new class -n {classSettings.Name} -o {classSettings.Directory}");

            var root = GetClassRoot(classSettings.Directory + "\\" + classSettings.Name).Value;
            SyntaxNode _activeNode = NameSpaceService.ChangeNameSpace(root, classSettings.Directory).Value;

            if (!string.IsNullOrEmpty(classSettings.BaseInheritance))
            {
                _activeNode = InheritanceService.SetBaseInheritance(_activeNode, classSettings.BaseInheritance).Value;
            }

            foreach (var usingText in classSettings.Usings)
            {
                _activeNode = UsingService.AddUsing(_activeNode, usingText).Value;
            }


            if (classSettings.Constructor)
            {
                _activeNode = ConstructorService.AddConstructor(_activeNode).Value;
            }

            if (!string.IsNullOrEmpty(classSettings.ConstructorParameters))
            {
                _activeNode = ConstructorService.AddConstructorParameters(_activeNode, classSettings.ConstructorParameters, classSettings.ConstructorBaseParameters).Value;
            }

            foreach (var code in classSettings.ConstructorCodes)
            {
                _activeNode = ConstructorService.AddConstructorCode(_activeNode, code).Value;
            }




            ReWriteClass(classSettings.Directory + "\\" + classSettings.Name, _activeNode);

            return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassCreated);
        }
    }
}
