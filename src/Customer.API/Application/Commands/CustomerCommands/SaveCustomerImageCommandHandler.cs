using CustomerMan.API.Application.Commands.CustomerCommands.Commands;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
using SharedKernel.Extentions.HttpResponse;
namespace CustomerMan.API.Application.Commands.CustomerCommands;
public class SaveCustomerImageCommand : CustomerCommand<string> { }

public class SaveCustomerImageCommandHandler : IRequestHandler<SaveCustomerImageCommand, string>
{
    private readonly ILogger<SaveCustomerImageCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public SaveCustomerImageCommandHandler(ICustomerRepository customerRepository, ILogger<SaveCustomerImageCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<string> Handle(SaveCustomerImageCommand command, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepository.GetByIdAsync(command.Id, cancellationToken);

        if (customer == null)
        {
            _logger.LogError("Failed to continue the process due to the given ID:{id} not found", command.Id);
            throw new CustomerManDomainException("The system couldn't find the target customer", HttpResponseType.NotFound);
        }

        //TODO: Add logic to handle the request for saving the image

        return string.Empty;
    }
}