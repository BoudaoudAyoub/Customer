using SharedKernel.Seedwork;
namespace SharedKernel.Models;
public class AudityInfo(Guid createdBy) : ValueObject
{
    public Guid CreatedBy { get; private set; } = createdBy;
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public Guid ModifiedBy { get; private set; } = default!;
    public DateTime ModifiedDate { get; private set; } = default!;
    public Guid DeletedBy { get; private set; } = default!;
    public DateTime DeletedDate { get; private set; } = default!;
    public Guid RestoredBy { get; private set; } = default!;
    public DateTime RestoredDate { get; private set; } = default!;

    public void LastModificationBy(Guid lastModifiedBy)
    {
        ModifiedBy = lastModifiedBy;
        ModifiedDate = DateTime.UtcNow;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return CreatedBy;
        yield return CreatedDate;
        yield return ModifiedBy;
        yield return ModifiedDate;
    }
}