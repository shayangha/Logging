using Logging.BaseLogger;
using Logging.Logger;
using Microsoft.AspNetCore.Http;
using NLog;
using LogLevel = Logging.BaseLogger.LogLevel;

namespace Logging.LoggerAdapter;

public class NLogLogger<T> : LoggerService<T>
{
    public NLogLogger
        (IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override void LogByLibrary(Log log, Exception exception)
    {
        var loggerMessage = log.ToString();

        var logger = LogManager.GetLogger(typeof(T).ToString());

        switch (log.Level)
        {
            case LogLevel.Trace:
            {
                logger.Trace(exception, loggerMessage);
                break;
            }

            case LogLevel.Debug:
            {
                logger.Debug(exception, loggerMessage);
                break;
            }

            case LogLevel.Information:
            {
                logger.Info(exception, loggerMessage);
                break;
            }

            case LogLevel.Warning:
            {
                logger.Warn(exception, loggerMessage);
                break;
            }

            case LogLevel.Error:
            {
                logger.Error(exception, loggerMessage);
                break;
            }

            case LogLevel.Fatal:
            {
                logger.Fatal(exception, loggerMessage);
                break;
            }
        }
    }
}