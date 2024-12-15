namespace CustomerMan.API.Application.Commands.CustomerCommands.Commands;
public abstract class CustomerCommand<TReponse> : IRequest<TReponse>
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IFormFile? Image { get; set; } = default!;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Coutry { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}