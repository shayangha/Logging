using System.Collections;

namespace Logging.Logger;

public interface ILoggerService<T>
{
    void LogTrace(string message, Hashtable parameters = null);

    void LogDebug(string message, Hashtable parameters = null);

    void LogInformation(string message, Hashtable parameters = null);

    void LogWarning(string message, Hashtable parameters = null);

    void LogError(Exception exception = null, string message = null, Hashtable parameters = null);

    void LogFatal(Exception exception = null, string message = null, Hashtable parameters = null);
}