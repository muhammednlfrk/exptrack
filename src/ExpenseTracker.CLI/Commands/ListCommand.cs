using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Core;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class ListSettings : CommandSettings
{
    [CommandOption("-i|--id <ID>")]
    public int? Id { get; set; }

    [CommandOption("--day <DAY>")]
    public int? Day { get; set; }

    [CommandOption("--month <MONTH>")]
    public int? Month { get; set; }

    [CommandOption("--year <YEAR>")]
    public int? Year { get; set; }
}

public sealed class ListCommand([NotNull] IExpenseService _expenseService) : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings)
    {
        ServiceResponse<IEnumerable<ExpenseEntity>> response = _expenseService.ListExpenses(settings.Day, settings.Month, settings.Year);
        if (response.Success)
        {
            if (response.Result?.Count() == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No expenses found.[/]");
                return 0;
            }

            // List as table
            Table table = new();
            table.AddColumn("ID");
            table.Columns[0].RightAligned();
            table.Columns[0].Padding(1, 1);
            table.Columns[0].NoWrap();
            table.AddColumn("Date");
            table.Columns[1].Centered();
            table.Columns[1].Padding(1, 1);
            table.Columns[1].NoWrap();
            table.AddColumn("Description");
            table.Columns[2].LeftAligned();
            table.Columns[2].Padding(1, 1);
            table.Columns[2].NoWrap();
            table.AddColumn("Amount");
            table.Columns[3].RightAligned();
            table.Columns[3].Padding(1, 1);
            table.Columns[3].NoWrap();

            table.Border(TableBorder.Ascii2);

            foreach (ExpenseEntity expense in response.Result ?? [])
            {
                table.AddRow(
                    expense.Id?.ToString() ?? "N/A",
                    expense.CreatedAt?.ToString("dd MMM yy") ?? "N/A",
                    expense.Description ?? "N/A",
                    "$" + string.Format("{0:N2}", expense.Amount));
            }

            table.Collapse();
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]{response.ErrorMessage}[/]");
        }

        return response.Success ? 0 : 1;
    }
}
