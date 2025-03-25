using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Helpers
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
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                string output;
                using (var process = Process.Start(startInfo))
                {
                    output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }

                return output;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
