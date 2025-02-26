using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class DeleteSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")]
    public required int Id { get; set; }

    [CommandOption("-f|--force")]
    public bool Forced { get; set; }
}

public class DeleteCommand([NotNull] IExpenseService _expenseService) : Command<DeleteSettings>
{
    public override int Execute(CommandContext context, DeleteSettings settings)
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
            AnsiConsole.MarkupLine($"[green]Expense with ID {settings.Id} deleted successfully[/]");
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Failed to delete expense with ID {settings.Id}[/]");
            return 1;
        }
    }
}
