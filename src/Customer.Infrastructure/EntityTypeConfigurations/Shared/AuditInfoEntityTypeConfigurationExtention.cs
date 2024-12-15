using SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CustomerMan.Infrastructure.EntityTypeConfigurations;
public class AuditInfoEntityTypeConfigurationExtention<T> where T : class
{
    public void AuditInfoEntityTypeConfiguration(OwnedNavigationBuilder<T, AudityInfo> builder)
    {
        builder.Property(a => a.CreatedBy).IsRequired().ValueGeneratedNever().HasColumnName("CreatedBy");
        builder.Property(a => a.CreatedDate).IsRequired().ValueGeneratedNever().HasColumnName("CreatedDate");
        builder.Property(a => a.ModifiedBy).IsRequired().ValueGeneratedNever().HasColumnName("ModifiedBy");
        builder.Property(a => a.ModifiedDate).IsRequired().ValueGeneratedNever().HasColumnName("ModifiedDate");
        builder.Property(a => a.DeletedBy).IsRequired().ValueGeneratedNever().HasColumnName("DeletedBy");
        builder.Property(a => a.DeletedDate).IsRequired().ValueGeneratedNever().HasColumnName("DeletedDate");
        builder.Property(a => a.RestoredBy).IsRequired().ValueGeneratedNever().HasColumnName("RestoredBy");
        builder.Property(a => a.RestoredDate).IsRequired().ValueGeneratedNever().HasColumnName("RestoredDate");
    }
}