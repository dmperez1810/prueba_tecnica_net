namespace OpenData.Model.Operation;

public class Error
{
    public Exception? Exception { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }

    public Error(string message, string code, Exception? exception = null)
    {
        Code = code;
        Message = message;
        Exception = exception;
    }

}