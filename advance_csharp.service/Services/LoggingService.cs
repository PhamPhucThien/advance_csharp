﻿using advance_csharp.service.Interfaces;
using log4net;


namespace advance_csharp.service.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILog _logger;

        public LoggingService()
        {
            _logger = LogManager.GetLogger(typeof(LoggingService));
        }

        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
