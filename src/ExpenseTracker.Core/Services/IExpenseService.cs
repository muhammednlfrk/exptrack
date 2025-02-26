namespace ExpenseTracker.Core.Services;

/// <summary>
/// Represents a service for managing expenses.
/// </summary>
public interface IExpenseService
{
    /// <summary>
    /// Gets the summary of expenses.
    /// </summary>
    /// <param name="day">The day of the year.</param>
    /// <param name="month">The month of the year.</param>
    /// <param name="year">The year.</param>
    /// <returns>A service response containing the list of expenses.</returns>
    ServiceResponse<float> SummaryExpenses(int? day, int? month, int? year);

    /// <summary>
    /// Lists all expenses.
    /// </summary>
    /// <returns>A service response containing the list of expenses.</returns>
    /// <param name="day">The day of the year.</param>
    /// <param name="month">The month of the year.</param>
    /// <param name="year">The year.</param>
    /// <returns>A service response containing the list of expenses.</returns>
    ServiceResponse<IEnumerable<ExpenseEntity>> ListExpenses(int? day, int? month, int? year);

    /// <summary>
    /// Gets an expense by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the expense.</param>
    /// <returns>A service response containing the expense.</returns>
    ServiceResponse<ExpenseEntity?> GetExpense(int id);

    /// <summary>
    /// Adds a new expense.
    /// </summary>
    /// <param name="description">The description of the expense.</param>
    /// <param name="amount">The amount of the expense.</param>
    ServiceResponse AddExpense(string description, float amount);

    /// <summary>
    /// Deletes an expense by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the expense.</param>
    /// <returns>A service response.</returns>
    ServiceResponse<ExpenseEntity?> UpdateExpense(int id, string? description, float? amount);

    /// <summary>
    /// Deletes an expense by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the expense.</param>
    /// <returns>A service response.</returns>
    ServiceResponse<bool> DeleteExpense(int id);

    /// <summary>
    /// Soft deletes an expense by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the expense.</param>
    /// <returns>A service response.</returns>
    ServiceResponse<bool> SoftDeleteExpense(int id);
}
