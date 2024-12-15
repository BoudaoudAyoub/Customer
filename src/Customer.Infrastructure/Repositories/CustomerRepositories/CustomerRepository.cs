using CustomerMan.Domain.AggregateModels.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
namespace CustomerMan.Infrastructure.Repositories.CustomerRepositories;
public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(CustomerManDbContext customerDbContext) : base(customerDbContext)
    {
    }

    public async Task<bool> DoesCustomExistsByName(string name) 
        => await DbSet.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()), CancellationToken.None);
}