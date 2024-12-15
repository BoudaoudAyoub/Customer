namespace SharedKernel.Exceptions;
public sealed class ExceptionRoot
{
    public string Instance { get; set; } = string.Empty;
    public string ControllerName { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public DateTime ExceptionLogTime { get; set; } = default!;
    public ExceptionDetails ErrorResponse { get; set; } = default!;
}