using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ExpenseTracker.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class SummarySettings : CommandSettings
{
    [Description("The day of the expense(s).")]
    [CommandOption("-d|--day")]
    public int? Day { get; set; }

    [Description("The month of the expense(s).")]
    [CommandOption("-m|--month")]
    public int? Month { get; set; }

    [Description("The year of the expense(s).")]
    [CommandOption("-y|--year")]
    public int? Year { get; set; }
}

public sealed class SummaryCommand([NotNull] IExpenseService _expenseService) : Command<SummarySettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] SummarySettings settings)
    {
        ServiceResponse<float> response = _expenseService.SummaryExpenses(settings.Day, settings.Month, settings.Year);

        if (response.Success)
        {
            StringBuilder dateBuidler = new();
            bool addSeperator = false;
            bool addFor = false;
            if (settings.Day.HasValue)
            {
                dateBuidler.Append(settings.Day?.ToString("D2"));
                addSeperator = true;
                addFor = true;
            }
            if (settings.Month.HasValue)
            {
                if (addSeperator) dateBuidler.Append('/');
                dateBuidler.Append(settings.Month?.ToString("D2"));
                addSeperator = true;
                addFor = true;
            }
            if (settings.Year.HasValue)
            {
                if (addSeperator) dateBuidler.Append('/');
                dateBuidler.Append(settings.Year?.ToString("D4"));
                addFor = true;
            }

            StringBuilder messageBuilder = new();
            messageBuilder.Append("Total expenses");
            if (addFor) messageBuilder.Append(" for ");
            messageBuilder.Append(dateBuidler);
            messageBuilder.Append(": [bold]");
            messageBuilder.Append(response.Result.ToString("C"));
            messageBuilder.Append("[/]");
            AnsiConsole.MarkupLine($"{messageBuilder}");
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[bold red]Error:[/] {response.ErrorMessage}");
            return 1;
        }
    }
}
