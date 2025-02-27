using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class DeleteSettings : CommandSettings
{
    [Description("The ID of the expense to delete.")]
    [CommandArgument(0, "<ID>")]
    public required int Id { get; set; }

    [Description("Delete the expense permanently.")]
    [CommandOption("-f|--force")]
    public bool Forced { get; set; }
}

public class DeleteCommand([NotNull] IExpenseService _expenseService) : Command<DeleteSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] DeleteSettings settings)
    {
        ServiceResponse<bool> response;
        if (settings.Forced)
        {
            response = _expenseService.DeleteExpense(settings.Id);
        }
        else
        {
            response = _expenseService.SoftDeleteExpense(settings.Id);
        }

        if (response.Success)
        {
            AnsiConsole.MarkupLine($"Expense with ID {settings.Id} deleted successfully.");
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[bold red]Error:[/] {response.ErrorMessage}");
            return 1;
        }
    }
}
