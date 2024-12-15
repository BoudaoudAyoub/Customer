using Microsoft.Data.SqlClient;
using CustomerMan.Infrastructure;
using SharedKernel.Extentions.HttpResponse;
using CustomerMan.API.Application.Models.CustomerModels;
namespace CustomerMan.API.Application.Queries.CustomerQueries;

public class CustomerQueries : ICustomerQueries
{
    private readonly ILogger<CustomerQueries> _logger;
    private readonly CustomerManDbContext _customerManDbContext;

    public CustomerQueries(ILogger<CustomerQueries> logger, CustomerManDbContext customerManDbContext)
    {
        _logger = logger ?? throw new CustomerManDomainException(nameof(logger));
        _customerManDbContext = customerManDbContext ?? throw new CustomerManDomainException(nameof(customerManDbContext));
    }

    public async Task<IEnumerable<SimpleCustomerModel>> GetAllCustomersAsync(CustomerFilter customerFilter)
    {
        if (customerFilter is null)
        {
            _logger.LogError("Failed to continue the process due to the object type of {type} cannot be null", typeof(CustomerFilter));
            throw new CustomerManDomainException($"{nameof(customerFilter)} cannot be null");
        }

        _logger.LogInformation("----Start getting all customers----");

        IEnumerable<SimpleCustomerModel> customers =
            _customerManDbContext.Customers.Where(customer => customer.Deleted == customerFilter.Deleted)
                                           .Select(customer => new SimpleCustomerModel()
                                           {
                                               Id = customer.ID,
                                               Name = customer.Name,
                                               ContactEmail = customer.ContactEmail,
                                               PhoneNumber = customer.Phone,
                                               IsActive = customer.IsActive,
                                               Image = customer.ImagePath
                                           });

        return await Task.FromResult(customers);
    }

    public async Task<CustomerDetailsModel> GetByIdAsync(Guid customerId)
    {
        bool customerExists = await _customerManDbContext.Customers
                                    .FromSqlRaw("SELECT ID FROM [customerMan].[customerContext].[Customers] WHERE ID=@id AND Deleted <> 1",
                                     new SqlParameter("@id", customerId))
                                    .AnyAsync();

        if (!customerExists)
        {
            _logger.LogError("Failed to continue the process due to the given ID:{id} not found", customerId);
            throw new CustomerManDomainException("The system couldn't find the target customer", HttpResponseType.NotFound);
        }

        _logger.LogInformation("----Get customer with unique identifier: {customer}", customerId);

        IQueryable<CustomerDetailsModel> customers =
            _customerManDbContext.Customers.Where(customer => customer.ID == customerId && !customer.Deleted)
                                           .Select(customer => new CustomerDetailsModel()
                                           {
                                               Id = customer.ID,
                                               Name = customer.Name,
                                               ContactEmail = customer.ContactEmail,
                                               PhoneNumber = customer.Phone,
                                               Description = customer.Description,
                                               CreatedDate = customer.AudityInfo.CreatedDate.ToString(),
                                               UpdatedDate = customer.AudityInfo.ModifiedDate.ToString(),
                                               Adress = new AdressModel()
                                               {
                                                   City = customer.Adress.City,
                                                   Country = customer.Adress.Country,
                                                   State = customer.Adress.State,
                                                   Street = customer.Adress.Street,
                                                   ZipCode = customer.Adress.ZipCode
                                               },
                                               IsDeleted = customer.Deleted,
                                               IsActive = customer.IsActive,
                                               DeletedDate = customer.Deleted ? customer.AudityInfo.DeletedDate.ToString() : string.Empty,
                                           });

        return await Task.FromResult(customers.Single());
    }
}