using SharedKernel.Models;
namespace SharedKernel.Seedwork;
/// <summary>
/// Defines an interface for entities that require audit tracking through 'AudityInfo'
/// This interface enforces consistency in naming and usage of audit-related properties and methods
/// </summary>
public interface IAudityInfo
{
    /// <summary>
    /// Gets the AudityInfo containing details about entity creation and modifications
    /// </summary>
    AudityInfo AudityInfo { get; }

    /// <summary>
    /// Records the creation of an entity along with the identifier of the creator
    /// </summary>
    /// <param name="createdBy">The unique identifier of the user who created the entity</param>
    void CreatedBy(Guid createdBy);

    /// <summary>
    /// Records the modification of an entity along with the identifier of the user making the modification
    /// </summary>
    /// <param name="lastModificationBy">The unique identifier of the user who modified the entity</param>
    void LastModificationBy(Guid lastModificationBy);

    void DeletedBy(Guid deletedBy);

    void RestoredBy(Guid updatedBy);
}