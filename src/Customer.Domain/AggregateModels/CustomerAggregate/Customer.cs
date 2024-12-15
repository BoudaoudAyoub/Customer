using SharedKernel.Models;
using SharedKernel.Seedwork;
using CustomerMan.Domain.Seedwork;
namespace CustomerMan.Domain.AggregateModels.CustomerAggregate;
public class Customer : Entity<Guid>, IAudityInfo, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string ContactEmail { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string ImagePath { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = default;
    public bool Deleted { get; private set; } = default;

    // DDD Patterns comment
    // Both Address and AudityInfo are implemented as Value Objects using the Entity Framework's owned entity feature
    public Adress Adress { get; private set; } = default!;
    public AudityInfo AudityInfo { get; private set; } = default!;

    public Customer(string name) => Name = name;

    public void SetBasicCustomerInformation(string contactEmail, string description, string phone, Adress adress) 
    {
        Phone = phone;
        Adress = adress;
        Description = description;
        ContactEmail = contactEmail;
    }

    public void SetImapgePath(string imagePath) => ImagePath = imagePath; 

    public void CreatedBy(Guid createdBy) => AudityInfo = new(createdBy);

    public void LastModificationBy(Guid lastModificationBy) => AudityInfo.LastModificationBy(lastModificationBy);

    public void DeletedBy(Guid deletedBy)
    {
        throw new NotImplementedException();
    }

    public void RestoredBy(Guid updatedBy)
    {
        throw new NotImplementedException();
    }

    public void MarkCustomerAsDeleted() => Deleted = true;

    public void Restore() => Deleted = false;
}