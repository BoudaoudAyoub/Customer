namespace CustomerMan.API.Application.Models.CustomerModels;

public class CustomerDetailsModel : CustomerModel
{
    public string CreatedDate { get; set; } = default!;
    public string UpdatedDate { get; set; } = default!; 
    public string DeletedDate { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public bool IsDeleted { get; set; } = default!;
}

public class SimpleCustomerModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public string Image { get; set; } = default!;
}

public class CustomerModel : BaseCustomerModel
{
    public Guid Id { get; set; } = Guid.Empty;
}

public class BaseCustomerModel
{
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = default!;
    public AdressModel Adress { get; set; } = default!;
}

public class AdressModel
{
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}