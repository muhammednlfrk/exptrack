namespace ExpenseTracker.Core;

/// <summary>
/// The base class for all entities in the application.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// The unique identifier for the entity.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// The date and time the entity was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The date and time the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// A flag indicating if the entity has been deleted.
    /// </summary>
    public bool? IsDeleted { get; set; }
}
