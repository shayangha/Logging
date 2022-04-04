using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using Logging.BaseLogger;
using Microsoft.AspNetCore.Http;

namespace Logging.Logger;

public abstract class LoggerService<T> : ILoggerService<T>
{
    private readonly IHttpContextAccessor HttpContextAccessor;

    #region Ctor

    public LoggerService(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    #endregion

    protected abstract void LogByFavoriteLibrary(Log log, Exception exception);


    #region GetExceptions

    protected virtual string GetExceptions(Exception exception)
    {
        var result = new StringBuilder();

        var currentException = exception;

        var index = 0;

        while (currentException != null)
        {
            if (index == 0)
                result.Append($"<{nameof(Exception)}>");
            else
                result.Append($"<{nameof(Exception.InnerException)}>");

            result.Append(currentException.Message);

            if (index == 0)
                result.Append($"</{nameof(Exception)}>");
            else
                result.Append($"</{nameof(Exception.InnerException)}>");
            index++;

            currentException =
                currentException.InnerException;
        }

        return result.ToString();
    }

    #endregion

    #region GetParameters

    protected virtual string GetParameters(Hashtable parameters)
    {
        if (parameters == null || parameters.Count == 0) return null;

        var stringBuilder = new StringBuilder();

        foreach (DictionaryEntry item in parameters)
            if (item.Key != null)
            {
                stringBuilder.Append("<parameter>");

                stringBuilder.Append($"<key>{item.Key}</key>");

                if (item.Value == null)
                    stringBuilder.Append("<value>NULL</value>");
                else
                    stringBuilder.Append($"<value>{item.Value}</value>");

                stringBuilder.Append("</parameter>");
            }

        var result =
            stringBuilder.ToString();

        return result;
    }

    #endregion

    #region Methods

    protected void Log(LogLevel level, MethodBase methodBase, string message, Exception exception = null,
        Hashtable parameters = null)
    {
        if (exception == null && string.IsNullOrWhiteSpace(message)) return;

        var currentCultureName =
            Thread.CurrentThread.CurrentCulture.Name;

        var newCultureInfo =
            new CultureInfo("en-US");

        var currentCultureInfo =
            new CultureInfo(currentCultureName);

        Thread.CurrentThread.CurrentCulture = newCultureInfo;

        var log = new Log();
        log.Level = level;

        log.ClassName = typeof(T).Name;
        log.MethodName = methodBase.Name;
        log.Namespace = typeof(T).Namespace;

        if (HttpContextAccessor != null &&
            HttpContextAccessor.HttpContext != null &&
            HttpContextAccessor.HttpContext.Connection != null &&
            HttpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
            log.IP =
                HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        if (HttpContextAccessor != null &&
            HttpContextAccessor.HttpContext != null &&
            HttpContextAccessor.HttpContext.User != null &&
            HttpContextAccessor.HttpContext.User.Identity != null)
            log.Username =
                HttpContextAccessor.HttpContext.User.Identity.Name;

        if (HttpContextAccessor != null &&
            HttpContextAccessor.HttpContext != null &&
            HttpContextAccessor.HttpContext.Request != null)
        {
            log.RequestPath =
                HttpContextAccessor.HttpContext.Request.Path;

            log.HttpReferrer =
                HttpContextAccessor.HttpContext.Request.Headers["Referer"];
        }

        log.Message = message;

        log.Exceptions = GetExceptions(exception);

        log.Parameters = GetParameters(parameters);

        LogByFavoriteLibrary(log, exception);

        Thread.CurrentThread.CurrentCulture = currentCultureInfo;
    }

    public void LogTrace(string message, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Trace, message: message, exception: null, parameters: parameters);
    }

    public void LogDebug(string message, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Debug, message: message, exception: null, parameters: parameters);
    }

    public void LogInformation(string message, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Information, message: message, exception: null,
            parameters: parameters);
    }

    public void LogWarning(string message, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Warning, message: message, exception: null, parameters: parameters);
    }

    public void LogError(Exception exception = null, string message = null, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Error, message: message, exception: null, parameters: parameters);
    }

    public void LogFatal(Exception exception = null, string message = null, Hashtable parameters = null)
    {
        var stackTrace = new StackTrace();

        var methodBase = stackTrace.GetFrame(1).GetMethod();

        Log(methodBase: methodBase, level: LogLevel.Fatal, message: message, exception: null, parameters: parameters);
    }

    #endregion
}