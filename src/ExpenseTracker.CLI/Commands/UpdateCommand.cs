using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class UpdateSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")]
    public int Id { get; set; }

    [CommandOption("-d|--description <DESCRIPTION>")]
    public string? Description { get; set; }

    [CommandOption("-a|--amount <AMOUNT>")]
    public float? Amount { get; set; }

    public override ValidationResult Validate()
    {
        if (Amount != null && Amount <= 0)
        {
            return ValidationResult.Error("Amount must be greater than zero.");
        }

        return base.Validate();
    }
}

public sealed class UpdateCommand([NotNull] IExpenseService _expenseService) : Command<UpdateSettings>
{
    public override int Execute(CommandContext context, UpdateSettings settings)
    {
        ServiceResponse<ExpenseEntity?> expenseRes = _expenseService.GetExpense(settings.Id);

        if (!expenseRes.Success)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {expenseRes.ErrorMessage}");
            return 1;
        }

        if (expenseRes.Result is null)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] Expense with ID {settings.Id} not found.");
            return 1;
        }

        ServiceResponse<ExpenseEntity?> response = _expenseService.UpdateExpense(expenseRes.Result.Id ?? -1, settings.Description, settings.Amount);

        if (response.Success)
        {
            AnsiConsole.MarkupLine($"[green]Success:[/] Expense with ID {response.Result?.Id} updated.");
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {response.ErrorMessage}");
            return 1;
        }
    }
}
