using CustomerMan.Domain.CustomerConstants;
using SharedKernel.Extentions.HttpResponse;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
using CustomerMan.API.Application.Commands.CustomerCommands.Commands;
namespace CustomerMan.API.Application.Commands.CustomerCommands;
public class DeleteCustomerCommand : CustomerCommand<bool> 
{
    public DeleteCustomerCommand(Guid id) => Id = id;
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<DeleteCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----Request to get customer with unique identifier: {id}", command.Id);

        Customer? customer = await _customerRepository.GetByIdAsync(command.Id, cancellationToken);

        if (customer == null)
        {
            _logger.LogError("----Coudn't find the customer with unique identifier: {id}", command.Id);
            throw new CustomerManDomainException(ErrorConstants.CUSTOMER_DOESNT_EXISTS, HttpResponseType.NotFound);
        }

        //TODO: add currentUser unique Identifier
        customer.LastModificationBy(Guid.NewGuid());

        if (customer.Deleted)
        {
            // If the customer is already marked as deleted, remove it permanently from the database
            _customerRepository.RemoveSingle(customer);
            _logger.LogInformation("----Deleted customer: {id}, {name} permanently", customer.ID, customer.Name);
        }
        else
        {
            // If the customer is not deleted yet, mark it as deleted and update its status in the database
            customer.MarkCustomerAsDeleted();
            _customerRepository.UpdateSingle(customer);
            _logger.LogInformation("----Deleted customer: {id}, {name} softly", customer.ID, customer.Name);
        }

        return await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}