using CustomerMan.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
namespace CustomerMan.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using CustomerMan.Infrastructure.EntityTypeConfigurations;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;

/// <remarks>
/// Add migrations using the following command inside the 'CustomerMan.Infrastructure' project directory:
/// entityFrameworkCore\add-migration [migration-name] --context CustomerManDbContext
/// </remarks>
public class CustomerManDbContext(DbContextOptions<CustomerManDbContext> dbContextOptions) : DbContext(dbContextOptions), IUnitOfWork
{
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// TODO:
    /// </summary>
    private IDbContextTransaction _currentTransaction = default!;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction is not null;

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /// Why "HasDefaultSchema" ?
        /// By default, Entity Framework Core uses the "dbo" schema in Microsoft SQL Server, 
        /// which is the default schema for a database. However, you can use the "HasDefaultSchema"
        /// method to specify a different schema. When you specify a schema using this method, 
        /// all the tables that are generated from your models will be created under that schema.
        modelBuilder.HasDefaultSchema("customerContext");

        modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
    }

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 

        // await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<IDbContextTransaction> StartTransactionAsync()
    {
        // If the given dbContextTransaction is not null then return the default value of "IDbContextTransaction" which is 'null'
        if (_currentTransaction is not null) return default!;

        // Initialize a new instance to the current transaction using Database.BeginTransactionAsync() EFC method
        _currentTransaction = await Database.BeginTransactionAsync();

        // Return the current transaction
        return _currentTransaction;
    }

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));
       
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            DisposeTransaction();
        }
    }

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    /// <summary>
    /// TODO: apply summary for this method
    /// </summary>
    private void DisposeTransaction()
    {
        if(_currentTransaction is null) return;
        _currentTransaction.Dispose();
        _currentTransaction = default!;
    }
}