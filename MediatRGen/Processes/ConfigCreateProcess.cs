using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediatRGen.Processes
{
    public class ConfigCreateProcess : BaseProcess
    {
        private readonly string _projectName;
        private readonly string _path;
        private Config? _configuration;

        public ConfigCreateProcess()
        {
            _configuration = FileHelpers.GetConfig();

            if (_configuration == null)
            {
                throw new FileException(LangHandler.Definitions().ConfigNotFound);
            }

            if (_configuration?.Modul != null)
            {
                throw new FileException(LangHandler.Definitions().ConfigExist);
            }

            _configuration.Version = System.Reflection.Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "";

            ModuleSystemActive();

            FileHelpers.UpdateConfig(_configuration);
            Console.WriteLine("Configuration Created...");
            Console.WriteLine(JsonSerializer.Serialize(_configuration));
        }

        private void ModuleSystemActive()
        {
            while (true)
            {
                Console.WriteLine(LangHandler.Definitions().ModuleActive + " (y / n)");
                string? response = Console.ReadLine();

                if (response?.ToLower() != "y" && response?.ToLower() != "n")
                    continue;

                if (response.ToLower() == "n")
                {
                    _configuration.UseModule = false;
                    break;
                }
                else
                {
                    _configuration.UseModule = true;
                    break;
                }
            }
        }
    }
}
