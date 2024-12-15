using AutoMapper;
using SharedKernel.Extentions.HttpResponse;
using CustomerMan.API.Application.Commands.CustomerCommands;
using CustomerMan.API.Application.Models.CustomerModels;
using CustomerMan.API.Application.Queries.CustomerQueries;
namespace CustomerMan.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICustomerQueries _customerQueries;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(IMapper mapper, ILogger<CustomersController> logger, IMediator mediator, ICustomerQueries customerQueries)
    {
        _mapper = mapper ?? throw new CustomerManDomainException(nameof(mapper));
        _logger = logger ?? throw new CustomerManDomainException(nameof(logger));
        _mediator = mediator ?? throw new CustomerManDomainException(nameof(mediator));
        _customerQueries = customerQueries ?? throw new CustomerManDomainException(nameof(customerQueries));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    [ProducesResponseType(typeof(int), HttpResponseType.InternalServerError)]
    public async Task<CustomerModel> CreateCustomer([FromBody] BaseCustomerModel createCustomer)
    {
        if (createCustomer is null)
        {
            _logger.LogError("----Create customer object {obj} cannot be null", nameof(createCustomer));
            throw new CustomerManDomainException(nameof(createCustomer));
        }

        CreateCustomerCommand command = _mapper.Map<CreateCustomerCommand>(createCustomer);

        return await _mediator.Send(command);
    }

    [HttpGet]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.NotFound)]
    [ProducesResponseType(typeof(int), HttpResponseType.NoContent)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    public async Task<IEnumerable<SimpleCustomerModel>> GetCustomers(CustomerFilter customerFilter)
    {
        return await _customerQueries.GetAllCustomersAsync(customerFilter);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.NotFound)]
    [ProducesResponseType(typeof(int), HttpResponseType.NoContent)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    public async Task<CustomerDetailsModel> GetCustomer(Guid id) => await _customerQueries.GetByIdAsync(id);

    //[HttpPut("{id:Guid}")]
    //[ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    //[ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    //public async Task<ActionResult> UpdateById(Guid id) => Ok(id);

    [HttpPut("{id:Guid}/upload-image")]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    public async Task<object> SaveCustomerImage(Guid id, [FromForm] IFormFile image)
    {
        return await _mediator.Send(new SaveCustomerImageCommand()
        {
            Id = id,
            Image = image
        });
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.NotFound)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    public async Task<object> DeleteCustomer(Guid id) => await _mediator.Send(new DeleteCustomerCommand(id));

    [HttpPut("Restore")]
    [ProducesResponseType(typeof(int), HttpResponseType.Ok)]
    [ProducesResponseType(typeof(int), HttpResponseType.NotFound)]
    [ProducesResponseType(typeof(int), HttpResponseType.BadRequest)]
    public async Task<object> RestoreCustomers([FromBody] List<Guid> customersIds)
    {
        return await _mediator.Send(new RestoreCustomerCommand(customersIds));
    }
}