using System.Text;

namespace Logging.BaseLogger;

public class Log : ILog
{
    #region Methods

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append($"<{nameof(Level)}>{Level}</{nameof(Level)}>");

        if (string.IsNullOrWhiteSpace(Namespace))
            builder.Append($"<{nameof(Namespace)}>NULL</{nameof(Namespace)}>");
        else
            builder.Append($"<{nameof(Namespace)}>{Namespace}</{nameof(Namespace)}>");

        if (string.IsNullOrWhiteSpace(ClassName))
            builder.Append($"<{nameof(ClassName)}>NULL</{nameof(ClassName)}>");
        else
            builder.Append($"<{nameof(ClassName)}>{ClassName}</{nameof(ClassName)}>");

        if (string.IsNullOrWhiteSpace(MethodName))
            builder.Append($"<{nameof(MethodName)}>NULL</{nameof(MethodName)}>");
        else
            builder.Append($"<{nameof(MethodName)}>{MethodName}</{nameof(MethodName)}>");


        if (string.IsNullOrWhiteSpace(IP))
            builder.Append($"<{nameof(IP)}>NULL</{nameof(IP)}>");
        else
            builder.Append($"<{nameof(IP)}>{IP}</{nameof(IP)}>");

        if (string.IsNullOrWhiteSpace(RequestPath))
            builder.Append($"<{nameof(RequestPath)}>NULL</{nameof(RequestPath)}>");
        else
            builder.Append($"<{nameof(RequestPath)}>{RequestPath}</{nameof(RequestPath)}>");

        if (string.IsNullOrWhiteSpace(HttpReferrer))
            builder.Append($"<{nameof(HttpReferrer)}>NULL</{nameof(HttpReferrer)}>");
        else
            builder.Append($"<{nameof(HttpReferrer)}>{HttpReferrer}</{nameof(HttpReferrer)}>");

        if (string.IsNullOrWhiteSpace(Username))
            builder.Append($"<{nameof(Username)}>NULL</{nameof(Username)}>");
        else
            builder.Append($"<{nameof(Username)}>{Username}</{nameof(Username)}>");


        if (string.IsNullOrWhiteSpace(Message))
            builder.Append($"<{nameof(Message)}>NULL</{nameof(Message)}>");
        else
            builder.Append($"<{nameof(Message)}>{Message}</{nameof(Message)}>");

        if (string.IsNullOrWhiteSpace(Exceptions))
            builder.Append($"<{nameof(Exceptions)}>NULL</{nameof(Exceptions)}>");
        else
            builder.Append($"<{nameof(Exceptions)}>{Exceptions}</{nameof(Exceptions)}>");

        if (string.IsNullOrWhiteSpace(Parameters))
            builder.Append($"<{nameof(Parameters)}>NULL</{nameof(Parameters)}>");
        else
            builder.Append($"<{nameof(Parameters)}>{Parameters}</{nameof(Parameters)}>");
        var result =
            builder.ToString();

        return result;
    }

    #endregion

    #region props

    #region File Details

    public string Namespace { get; set; }
    public string ClassName { get; set; }
    public string MethodName { get; set; }

    #endregion

    #region Http Details

    public string IP { get; set; }
    public string Username { get; set; }
    public string RequestPath { get; set; }
    public string HttpReferrer { get; set; }

    #endregion

    #region General Details

    public string Message { get; set; }

    public string Parameters { get; set; }

    public string Exceptions { get; set; }
    public LogLevel Level { get; set; }

    #endregion

    #endregion
}