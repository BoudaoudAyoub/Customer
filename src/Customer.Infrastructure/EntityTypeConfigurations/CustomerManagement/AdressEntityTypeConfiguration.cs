using Microsoft.EntityFrameworkCore;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CustomerMan.Infrastructure.EntityTypeConfigurations;
public class AdressEntityTypeConfiguration<T> where T : class
{
    public void AdressEntityTypeConfig(OwnedNavigationBuilder<T, Adress> builder)
    {
        builder.Property(a => a.City).HasDefaultValue(string.Empty).HasColumnName("City");
        builder.Property(a => a.Country).HasDefaultValue(string.Empty).HasColumnName("Country");
        builder.Property(a => a.ZipCode).HasDefaultValue(string.Empty).HasColumnName("ZipCode");
        builder.Property(a => a.Street).HasDefaultValue(string.Empty).HasColumnName("Street");
        builder.Property(a => a.State).HasDefaultValue(string.Empty).HasColumnName("State");
    }
}