using MediatRGen.Cli.Schemas;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions.FileExceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.MediatR
{
    public class CreateMediatRCommand : Command<CreateServiceSchema>
    {
        private readonly IMediatRService _mediatRSErvice;

        public CreateMediatRCommand(IMediatRService mediatRSErvice)
        {
            _mediatRSErvice = mediatRSErvice;
        }

        public override int Execute(CommandContext context, CreateServiceSchema settings)
        {
            var options = settings.OptionsSet();

            _mediatRSErvice.Create(options);
            return 0;
        }
    }
}
