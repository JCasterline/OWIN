using System;

namespace OwinIdentitySqlServerRepository
{
    public class CustomOwinLoggerFactory : Microsoft.Owin.Logging.ILoggerFactory
    {
        private readonly LoggerFactory.LoggerFactory _logManager;

        public CustomOwinLoggerFactory(LoggerFactory.LoggerFactory logManager)
        {
            if (logManager == null) throw new ArgumentNullException("logManager");
            _logManager = logManager;
        }

        public Microsoft.Owin.Logging.ILogger Create(string name)
        {
            return new CustomOwinLogger(name, _logManager);
        }
    }
}