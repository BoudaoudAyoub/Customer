using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Extentions.HttpResponse;
using System;
namespace CustomerMan.API.Infrastructure.Filters;
public class CustomActionFilter : IActionFilter
{
    private readonly ILogger<CustomActionFilter> _logger;
    public CustomActionFilter(ILogger<CustomActionFilter> logger) => _logger = logger;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Code to run after the action method is executed
        _logger.LogInformation("Executing action method {ActionName} with parameters:", context.ActionDescriptor.DisplayName);


        if (context.Result is ObjectResult result)
        {
            string controller = context.RouteData.Values["controller"]?.ToString() ?? string.Empty;

            if (result.Value == null ||
               (result.Value is IList<object> o1 && o1.IsNullOrEmpty()) ||
               (result.Value is IEnumerable<object> o2 && o2.IsNullOrEmpty()))
            {
                result.StatusCode = HttpResponseType.NoContent;
            }
            else
            {
                string action = context.RouteData.Values["action"]?.ToString() ?? string.Empty;
                result.StatusCode = action switch
                {
                    var value when value.StartsWith(HttpRequestMethods.Create, StringComparison.OrdinalIgnoreCase) => HttpResponseType.Created,
                    var value when value.StartsWith(HttpRequestMethods.Get, StringComparison.OrdinalIgnoreCase) => HttpResponseType.Ok,
                    var value when value.StartsWith(HttpRequestMethods.Update, StringComparison.OrdinalIgnoreCase) => HttpResponseType.Ok,
                    var value when value.StartsWith(HttpRequestMethods.Delete, StringComparison.OrdinalIgnoreCase) => HttpResponseType.Ok,
                    _ => HttpResponseType.Unknown
                };
            }

            _logger.LogTrace($"Request ended from {controller} with status {result.StatusCode} and message {GetResultMessage(result.StatusCode)}");

            result.Value = new
            {
                Message = string.Join(controller, GetResultMessage(result.StatusCode)),
                Data = result.Value
            };
        }
    }

    private static string GetResultMessage(int? statusCode)
    {
        return (statusCode) switch
        {
            200 => HttpResponseMessageType.Ok,
            201 => HttpResponseMessageType.Created,
            202 => HttpResponseMessageType.Accepted,
            204 => HttpResponseMessageType.NoContent,
            404 => HttpResponseMessageType.NotFound,
            _ => HttpResponseMessageType.Unknown,
        };
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Code to run before the action method is executed
    }
}