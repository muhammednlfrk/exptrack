using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class AddSettings : CommandSettings
{
    [CommandOption("-d|--description <DESCRIPTION>")]
    public required string Description { get; set; }

    [CommandOption("-a|--amount <AMOUNT>")]
    public required float Amount { get; set; }

    public override ValidationResult Validate()
    {
        if (Amount <= 0)
        {
            return ValidationResult.Error("Amount must be greater than zero.");
        }
        else if (string.IsNullOrWhiteSpace(Description))
        {
            return ValidationResult.Error("Description must not be empty.");
        }

        return base.Validate();
    }
}

public sealed class AddCommand([NotNull] IExpenseService _expenseService) : Command<AddSettings>
{
    public override int Execute(CommandContext context, AddSettings settings)
    {
        ServiceResponse response = _expenseService
            .AddExpense(settings.Description, settings.Amount);

        if (response.Success)
        {
            AnsiConsole.MarkupLine($"[green]Expense added successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Failed to add expense: {response.ErrorMessage}[/]");
        }

        return response.Success ? 0 : 1;
    }
}
