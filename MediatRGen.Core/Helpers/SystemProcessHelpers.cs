using MediatRGen.Core.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Helpers
{
    public static class SystemProcessHelpers
    {
        public static string InvokeCommand(string parameters, string command = "dotnet")
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

                return output == "" ? error : output;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void BuildProject()
        {

            string res3 = InvokeCommand($"dotnet build {DirectoryHelpers.GetCurrentDirectory()}{GlobalState.Instance.ProjectName}.sln");
            Console.WriteLine(res3);

        }
    }
}
