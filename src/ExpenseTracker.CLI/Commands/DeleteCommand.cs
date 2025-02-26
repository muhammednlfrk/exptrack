using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class DeleteSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")]
    public required int Id { get; set; }

    [CommandOption("-f|--force")]
    public bool Force { get; set; }
}

public class DeleteCommand : Command<DeleteSettings>
{
    public override int Execute(CommandContext context, DeleteSettings settings)
    {
        Console.WriteLine($"Deleting expense with ID {settings.Id}...");
        return 0;
    }
}
