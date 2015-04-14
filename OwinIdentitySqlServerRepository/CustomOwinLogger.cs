using System;
using System.Diagnostics;
using LoggerFactory;
using ILogger = Microsoft.Owin.Logging.ILogger;

namespace OwinIdentitySqlServerRepository
{
    public class CustomOwinLogger : ILogger
    {
        private readonly string _name;
        private readonly LoggerFactory.LoggerFactory _logManager;

        public CustomOwinLogger(string name, LoggerFactory.LoggerFactory logManager)
        {
            _name = name;
            _logManager = logManager;
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var log = _logManager.CreateInstance(_name);

            var entry = new LogItem()
            {
                Message = formatter(state, exception),
                Exception = exception,
            };

            switch (eventType)
            {
                case TraceEventType.Critical:
                    entry.LogLevel = LogLevel.Fatal;
                    break;
                case TraceEventType.Error:
                    entry.LogLevel = LogLevel.Error;
                    break;
                case TraceEventType.Warning:
                    entry.LogLevel = LogLevel.Warn;
                    break;
                case TraceEventType.Information:
                    entry.LogLevel = LogLevel.Info;
                    break;
                default:
                    entry.LogLevel = LogLevel.Debug;
                    throw new ArgumentOutOfRangeException("eventType");
            }


            log.Log(entry);

            return true;

        }
    }
}
