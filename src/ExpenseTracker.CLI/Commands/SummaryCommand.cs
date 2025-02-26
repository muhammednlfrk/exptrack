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

public sealed class SummaryCommand : Command<SummarySettings>
{
    public override int Execute(CommandContext context, SummarySettings settings)
    {
        Console.WriteLine("Summary command executed");
        return 0;
    }
}
