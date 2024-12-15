using SharedKernel.Extentions.HttpResponse;

namespace SharedKernel.Exceptions;
public abstract class CustomException : Exception
{
    public int StatusCode { get; set; }
    public IDictionary<int, string[]>? Errors { get; }


    public CustomException() { }

    public CustomException(string message, IDictionary<int, string[]>? errors, int statusCode = HttpResponseType.BadRequest) 
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public CustomException(string message) : this(message, null) { }

    public CustomException(string message, int statusCode) : this(message, null, statusCode) { }

    public CustomException(IDictionary<int, string[]> errors) : this(string.Empty, errors) { }

    public CustomException(string message, Exception innerException) : base(message, innerException) { }
}