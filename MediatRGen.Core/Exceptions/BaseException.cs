using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Services;
using Microsoft.Extensions.DependencyInjection;
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
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

        private static void HandleException(InvalidCommandException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

        private static void HandleException(FileException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }
        private static void HandleException(DirectoryException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }
        private static void HandleException(LanguageNotFoundException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

        private static void HandleException(InvalidParameterException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

        private static void HandleException(ParameterParseException ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

        private static void HandleException(Exception ex)
        {
            var outputService = GlobalServices.Provider?.GetService<IOutputService>();

            if (outputService != null)
            {
                outputService.Error(ex.Message);
            }
        }

    }


}
