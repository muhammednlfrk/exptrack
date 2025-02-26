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

public sealed class ListCommand : Command<ListSettings>
{
    public override int Execute(CommandContext context, ListSettings settings)
    {
        if (settings.Id.HasValue)
        {
            Console.WriteLine($"Listing expense with ID: {settings.Id}");
        }
        else if (settings.Day.HasValue && settings.Month.HasValue && settings.Year.HasValue)
        {
            Console.WriteLine($"Listing expenses for {settings.Day}/{settings.Month}/{settings.Year}");
        }
        else
        {
            Console.WriteLine("Listing all expenses");
        }

        return 0;
    }
}
