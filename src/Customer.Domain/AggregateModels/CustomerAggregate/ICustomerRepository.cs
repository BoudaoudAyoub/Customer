using CustomerMan.Domain.Seedwork;
namespace CustomerMan.Domain.AggregateModels.CustomerAggregate;
public interface ICustomerRepository : IRepository<Customer, Guid>
{
   Task<bool> DoesCustomExistsByName(string name);
}