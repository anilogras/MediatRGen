using MediatRGen.Core.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli
{
    internal class OutputService : IOutputService
    {
        public void Error(string message)
        {
            AnsiConsole.MarkupLine($"[green][[INFO]][/] {message}");
        }

        public void Info(string message)
        {
            AnsiConsole.MarkupLine($"[red][[ERROR]][/] {message}");
        }
    }
}
