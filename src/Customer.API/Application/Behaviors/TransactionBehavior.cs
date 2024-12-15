using MediatR;
using CustomerMan.Infrastructure;
using CustomerMan.Domain.DomainExceptions;
using Microsoft.EntityFrameworkCore.Storage;
namespace CustomerMan.API.Application.Behaviors;
public class TransactionBehavior<TRequest, TResponse>(CustomerManDbContext customerManDbContext, ILogger<TransactionBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger = logger ?? throw new ArgumentException(nameof(Serilog.ILogger));
    private readonly CustomerManDbContext _customerManDbContext = customerManDbContext ?? throw new ArgumentException(nameof(CustomerManDbContext));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = default!;

        string typeName = (typeof(TRequest)).Name;

        try
        {
            if (_customerManDbContext.HasActiveTransaction) await next();

            IExecutionStrategy strategy = _customerManDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _customerManDbContext.StartTransactionAsync();

                _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                response = await next();

                _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                await _customerManDbContext.CommitTransactionAsync(transaction);
            });

            return response;
        }
        catch (CustomerManDomainException ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }
}