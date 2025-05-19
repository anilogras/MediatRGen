using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Exceptions
{
    public static class BaseException
    {
        public static void ExceptionHandler(Exception exception)
        {
            switch (exception)
            {

                case InvalidParameterException invalidParameterException:
                    HandleException(invalidParameterException);
                    break;

                case InvalidCommandException invalidCommandException:
                    HandleException(invalidCommandException);
                    break;

                case LanguageNotFoundException languageNotFoundException:
                    HandleException(languageNotFoundException);
                    break;

                case ParameterParseException parameterParseException:
                    HandleException(parameterParseException);
                    break;

                case DirectoryException parameterParseException:
                    HandleException(parameterParseException);
                    break;

                case FileException parameterParseException:
                    HandleException(parameterParseException);
                    break;

                case ModuleException moduleParseException:
                    HandleException(moduleParseException);
                    break;

                default:
                    HandleException(exception);
                    break;
            }
        }
        private static void HandleException(ModuleException ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void HandleException(InvalidCommandException ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void HandleException(FileException ex)
        {
            Console.WriteLine(ex.Message);
        }
        private static void HandleException(DirectoryException ex)
        {
            Console.WriteLine(ex.Message);
        }
        private static void HandleException(LanguageNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void HandleException(InvalidParameterException ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void HandleException(ParameterParseException ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void HandleException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }


}
