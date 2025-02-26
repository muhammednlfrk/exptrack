namespace ExpenseTracker.Core;

/// <summary>
/// Represents an expense entity.
/// </summary>
public sealed class ExpenseEntity : EntityBase
{
    /// <summary>
    /// The description of the expense.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The amount of the expense.
    /// </summary>
    public float? Amount { get; set; }
}
