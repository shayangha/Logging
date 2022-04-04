namespace Logging.BaseLogger;

public interface ILog
{

    #region File Details
    string Namespace { get; set; }

    string ClassName { get; set; }

    string MethodName { get; set; }
    #endregion
    
    #region  Http Details
    string IP { get; set; }

    string Username { get; set; }

    string RequestPath { get; set; }

    string HttpReferrer { get; set; }
    #endregion


    #region General Details
    string Message { get; set; }

    string Parameters { get; set; }

    string Exceptions { get; set; }

    LogLevel Level { get; set; }

    #endregion
  
}