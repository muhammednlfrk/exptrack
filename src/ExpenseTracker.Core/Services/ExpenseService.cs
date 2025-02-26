
namespace ExpenseTracker.Core.Services;

/// <summary>
/// Represents a service for managing expenses.
/// </summary>
public sealed class ExpenseService(IExpenseRepository expenseRepository) : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));

    #region IExpenseRepository Implementation

    public ServiceResponse<float> SummaryExpenses(int? day, int? month, int? year)
    {
        List<ExpenseEntity> expenses = _expenseRepository.GetAllAsync(day, month, year).Result.ToList();

        if (expenses.Count == 0)
            return ServiceResponse<float>.Ok(0.0f);

        float total = expenses.Sum(e => e.Amount) ?? 0.0f;
        return ServiceResponse<float>.Ok(total);
    }

    public ServiceResponse<IEnumerable<ExpenseEntity>> ListExpenses(int? day, int? month, int? year)
    {
        if (!day.HasValue && !month.HasValue && !year.HasValue)
        {
            IEnumerable<ExpenseEntity> expenses = _expenseRepository.GetAllAsync().Result;
            return ServiceResponse<IEnumerable<ExpenseEntity>>.Ok(expenses);
        }
        else
        {
            IEnumerable<ExpenseEntity> expenses = _expenseRepository.GetAllAsync(day, month, year).Result;
            return ServiceResponse<IEnumerable<ExpenseEntity>>.Ok(expenses);
        }
    }

    public ServiceResponse<ExpenseEntity?> GetExpense(int id)
    {
        ExpenseEntity? expense = _expenseRepository.GetByIdAsync(id).Result;
        return ServiceResponse<ExpenseEntity?>.Ok(expense);
    }

    public ServiceResponse AddExpense(string description, float amount)
    {
        ExpenseEntity expense = new()
        {
            Description = description,
            Amount = amount,
            CreatedAt = DateTime.Now,
            IsDeleted = false,
        };

        try
        {
            _expenseRepository.AddAsync(expense).Wait();
            return ServiceResponse.Ok();
        }
        catch (Exception ex)
        {
            return ServiceResponse.Error(ex.Message);
        }
    }

    public ServiceResponse<ExpenseEntity?> UpdateExpense(int id, string? description, float? amount)
    {
        ExpenseEntity? expense = _expenseRepository.GetByIdAsync(id).Result;

        if (expense == null)
            return ServiceResponse<ExpenseEntity?>.Error("Expense not found.");

        expense.Description = description ?? expense.Description;
        expense.Amount = amount ?? expense.Amount;

        try
        {
            _expenseRepository.UpdateAsync(expense).Wait();
            return ServiceResponse<ExpenseEntity?>.Ok(expense);
        }
        catch (Exception ex)
        {
            return ServiceResponse<ExpenseEntity?>.Error(ex.Message);
        }
    }

    public ServiceResponse<bool> DeleteExpense(int id)
    {
        ExpenseEntity? expense = _expenseRepository.GetByIdAsync(id).Result;

        if (expense == null)
            return ServiceResponse<bool>.Error("Expense not found.");

        try
        {
            _expenseRepository.DeleteAsync(id).Wait();
            return ServiceResponse<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Error(ex.Message);
        }
    }

    public ServiceResponse<bool> SoftDeleteExpense(int id)
    {
        ExpenseEntity? expense = _expenseRepository.GetByIdAsync(id).Result;

        if (expense == null)
            return ServiceResponse<bool>.Error("Expense not found.");

        expense.IsDeleted = true;
        expense.UpdatedAt = DateTime.Now;

        try
        {
            _expenseRepository.UpdateAsync(expense).Wait();
            return ServiceResponse<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return ServiceResponse<bool>.Error(ex.Message);
        }
    }


    #endregion
}