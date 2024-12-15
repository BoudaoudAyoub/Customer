using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
namespace CustomerMan.Infrastructure.EntityTypeConfigurations;
public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.ID);
        builder.Property(customer => customer.Name).IsRequired();
        builder.Property(customer => customer.ContactEmail);
        builder.Property(customer => customer.Description);
        builder.Property(customer => customer.Deleted);
        builder.Property(customer => customer.ImagePath);
        builder.Property(customer => customer.IsActive);

        builder.OwnsOne(customer => customer.Adress, adress =>
        {
            new AdressEntityTypeConfiguration<Customer>().AdressEntityTypeConfig(adress);
        });

        builder.OwnsOne(customer => customer.AudityInfo, audit =>
        {
            new AuditInfoEntityTypeConfigurationExtention<Customer>().AuditInfoEntityTypeConfiguration(audit);
        });
    }
}