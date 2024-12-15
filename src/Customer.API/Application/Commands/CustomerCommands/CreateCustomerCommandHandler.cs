using AutoMapper;
using CustomerMan.Domain.CustomerConstants;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
using SharedKernel.Extentions.HttpResponse;
using CustomerMan.API.Application.Models.CustomerModels;
using CustomerMan.API.Application.Commands.CustomerCommands.Commands;
namespace CustomerMan.API.Application.Commands.CustomerCommands;
public class CreateCustomerCommand : CustomerCommand<CustomerModel> { }

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerModel>
{
    private readonly ILogger<CreateCustomerCommandHandler> _createCustomerCommandHandlerLogger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> createCustomerCommandHandlerLogger,
                                        ICustomerRepository customerRepository,
                                        IMapper mapper)
    {
        _createCustomerCommandHandlerLogger = createCustomerCommandHandlerLogger;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        if (await _customerRepository.DoesCustomExistsByName(command.Name))
        {
            _createCustomerCommandHandlerLogger.LogWarning("------Customer {@Name} already exists", command.Name);
            throw new CustomerManDomainException(string.Format(ErrorConstants.CUSTOMER_ALREADY_EXISTS, command.Name), HttpResponseType.Unauthorized);
        }

        Customer customer = new(command.Name);

        SetCustomerBasicInformation(ref customer, command);

        //TODO: change the image to file and add a check on
        // use local storage to store the image
        //customer.SetImapgePath(command.Image);

        //TODO: change this when you add the JWT configuration
        customer.CreatedBy(Guid.NewGuid());

        await _customerRepository.AddSingleAsync(customer, cancellationToken);

        _createCustomerCommandHandlerLogger.LogInformation("----- CreateCustomer new customer - customer: {@command}", command);

        await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerModel>(customer);
    }

    private static void SetCustomerBasicInformation(ref Customer customer, CreateCustomerCommand command)
    {
        Adress adress = new(command.Street, command.City, command.State, command.Coutry, command.ZipCode);

        customer.SetBasicCustomerInformation(command.ContactEmail, command.Description, command.PhoneNumber, adress);
    }
}