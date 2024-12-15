using Microsoft.IdentityModel.Tokens;
using CustomerMan.Domain.CustomerConstants;
using SharedKernel.Extentions.HttpResponse;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
namespace CustomerMan.API.Application.Commands.CustomerCommands;

public class RestoreCustomerCommand(List<Guid> customersIds) : IRequest<int> 
{
    public List<Guid> CustomersIds { get; set; } = customersIds;
}

public class RestoreCustomerCommandHandler : IRequestHandler<RestoreCustomerCommand, int>
{
    private readonly ILogger<RestoreCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public RestoreCustomerCommandHandler(ILogger<RestoreCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }


    public async Task<int> Handle(RestoreCustomerCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----Start looking for the customers IDs: {customersIds}----", command.CustomersIds);

        List<Customer> customers = await _customerRepository.GetAllAsQueryable()
                                                            .Where(customer => command.CustomersIds.Contains(customer.ID) && customer.Deleted)
                                                            .ToListAsync();

        if (customers.IsNullOrEmpty())
        {
            _logger.LogError("----Coudn't find any customer from the unique identifier list: {customersIds}----", command.CustomersIds);
            throw new CustomerManDomainException(ErrorConstants.TO_RESTORE_CUSTOMER, HttpResponseType.BadRequest);
        }

        customers.ForEach(customer =>
        {
            customer.Restore();
            //customer.RestoredBy(Guid.NewGuid());
        });

        _logger.LogTrace("----Restoring customers IDs: {customersIds}----", command.CustomersIds);

        _customerRepository.UpdateAll(customers);

        return await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}