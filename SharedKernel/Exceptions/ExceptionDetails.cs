using SharedKernel.Extentions.HttpResponse;

namespace SharedKernel.Exceptions;
public sealed class ExceptionDetails
{
    public int StatusCode { get; set; } = HttpResponseType.BadRequest;
    public string? Error { get; set; } = default!;
    public List<MultiErrors>? Errors { get; set; } = default!;
}

public sealed class MultiErrors
{
    public int StatusCode { get; set; } = default!;
    public string[] Errors { get; set; } = default!;
}