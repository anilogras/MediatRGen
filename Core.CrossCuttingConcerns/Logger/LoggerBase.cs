using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logger
{
    public class LoggerBase
    {
        private readonly ILogger _logger;

        public LoggerBase(ILogger logger)
        {
            _logger = logger;
        }

        public void Verbose(string message) => _logger.Verbose(message);

        public void Fatal(string message) => _logger.Fatal(message);

        public void Info(string message) => _logger.Information(message);

        public void Warn(string message) => _logger.Warning(message);

        public void Debug(string message) => _logger.Debug(message);

        public void Error(string message) => _logger.Error(message);
    }
}
