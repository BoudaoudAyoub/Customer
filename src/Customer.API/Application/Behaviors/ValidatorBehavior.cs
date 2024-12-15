using MediatR;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using CustomerMan.Domain.DomainExceptions;
using SharedKernel.Extentions.HttpResponse;
namespace CustomerMan.API.Application.Behaviors;
public class ValidatorBehavior<TRequest, TResponse>(ILogger<ValidatorBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest
    : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger = logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeName = typeof(TResponse).Name;

        _logger.LogInformation("-----Validating command {CommandType} :", typeName);

        var validationFailures = _validators.Select(validator => validator.Validate(request))
                                            .SelectMany(validator => validator.Errors)
                                            .Where(failure => failure is not null)
                                            .ToList();

        if (!validationFailures.IsNullOrEmpty())
        {
            _logger.LogWarning("Validation failure - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, validationFailures);

            throw new CustomerManDomainException(new Dictionary<int, string[]>()
            {
                {
                    HttpResponseType.BadRequest,
                    validationFailures.Select(err => $"{err.PropertyName} : {err.ErrorMessage}").ToArray()
                }
            });
        }

        return await next();
    }
}