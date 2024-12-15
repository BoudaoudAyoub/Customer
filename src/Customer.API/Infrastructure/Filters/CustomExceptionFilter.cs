﻿using SharedKernel.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Extentions.HttpResponse;
namespace CustomerMan.API.Infrastructure.Filters;
public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _exceptionFilterLogger = default!;
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> exceptionFilterLogger)
    {
        _exceptionFilterLogger = exceptionFilterLogger;
    }

    public void OnException(ExceptionContext context)
    {
        // Check whether the exception is handled by any of the filters in the application
        if (context is null || context.ExceptionHandled || context.Exception is NullReferenceException) return;

        ExceptionRoot exceptionRoot = new()
        {
            ExceptionLogTime = DateTime.UtcNow,
            Instance = context.HttpContext.Request.Path,
            ActionName = context.RouteData.Values["action"]?.ToString() ?? string.Empty,
            ControllerName = context.RouteData.Values["controller"]?.ToString() ?? string.Empty
        };

        if (context.Exception is CustomerManDomainException exception)
        {
            exceptionRoot.ErrorResponse = exception.Errors.IsNullOrEmpty() ? new()
            {
                StatusCode = exception.StatusCode,
                Error = exception.Message
            }
            :
            new()
            {
                Errors = exception.Errors?.Select(exception => new MultiErrors()
                {
                    StatusCode = exception.Key,
                    Errors = exception.Value.ToArray()

                }).ToList()
            };
        }
        else
        {
            exceptionRoot.ErrorResponse = new()
            {
                StatusCode = HttpResponseType.InternalServerError,
                Error = string.Format($"{SharedKernel.Extentions.HttpResponse.HttpResponseMessageType.InternalServerError} : {context.Exception.Message}")
            };
        }

        context.ExceptionHandled = true;
        context.Result = new ObjectResult(new { Failures = exceptionRoot });
        _exceptionFilterLogger.LogError(new EventId(context.Exception.HResult), string.Format("@exceptionRoot", exceptionRoot));
    }
}