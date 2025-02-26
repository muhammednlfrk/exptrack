using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class SummarySettings : CommandSettings
{
    [CommandOption("-d|--day")]
    public int? Day { get; set; }

    [CommandOption("-m|--month")]
    public int? Month { get; set; }

    [CommandOption("-y|--year")]
    public int? Year { get; set; }
}

public sealed class SummaryCommand([NotNull] IExpenseService _expenseService) : Command<SummarySettings>
{
    public override int Execute(CommandContext context, SummarySettings settings)
    {
        var response = _expenseService.SummaryExpenses(settings.Day, settings.Month, settings.Year);
        if (response.Success)
        {
            AnsiConsole.MarkupLine($"Total expenses: [bold]${response.Result}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Failed to retrieve summary of expenses: {response.ErrorMessage}[/]");
        }

        return response.Success ? 0 : 1;
    }
}
