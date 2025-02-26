namespace ExpenseTracker.Core;

/// <summary>
/// Interface for expense repository.
/// </summary>
public interface IExpenseRepository : IRepository<ExpenseEntity>
{
    /// <summary>
    /// Get all expenses by date.
    /// </summary>
    /// <param name="day">The day of the year.</param>
    /// <param name="month">The month of the year.</param>
    /// <param name="year">The year</param>
    /// <returns>The all expenses that have all 3 parameters.</returns>
    Task<IEnumerable<ExpenseEntity>> GetAllAsync(int? day, int? month, int? year);
}
