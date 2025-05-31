using MediatRGen.Core.Exceptions;
using MediatRGen.Services.Base;
using System.Diagnostics;

namespace MediatRGen.Services.HelperServices
{
    public class SystemProcessService
    {
        public static ServiceResult<string> InvokeCommand(string parameters, string command = "dotnet")
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = parameters,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                string output;
                string error;
                using (var process = Process.Start(startInfo))
                {
                    output = process.StandardOutput.ReadToEnd();
                    error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
                var result = output == "" ? error : output;
                return new ServiceResult<string>(result, true, DirectoryServices.ClearTwiceBackSlash(startInfo.Arguments.ToString()).Value + "\n" + LangHandler.Definitions().ProcessInvoked);
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>(null, false, LangHandler.Definitions().ProcessInvokeError, new SystemProcessException(ex.Message));
            }

        }
        public static ServiceResult<bool> BuildProject(string projectName)
        {
            try
            {
                string res3 = InvokeCommand($"dotnet build {DirectoryServices.GetCurrentDirectory().Value}{projectName}.sln").Value;
                return new ServiceResult<bool>(true, true, LangHandler.Definitions().ClassLibraryBuild + "\n" + res3, null);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, false, LangHandler.Definitions().ClassLibraryBuildError, new SystemProcessException(ex.Message));
            }

        }
    }
}
