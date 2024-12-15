using CustomerMan.API.Application.Models.CustomerModels;
namespace CustomerMan.API.Application.Queries.CustomerQueries;
public interface ICustomerQueries
{
    Task<IEnumerable<SimpleCustomerModel>> GetAllCustomersAsync(CustomerFilter customerFilter);

    Task<CustomerDetailsModel> GetByIdAsync(Guid customerId);
}