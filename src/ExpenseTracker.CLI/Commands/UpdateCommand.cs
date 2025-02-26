using Spectre.Console;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Commands;

public class UpdateSettings : CommandSettings
{
    [CommandArgument(0, "<ID>")]
    public int Id { get; set; }

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

public sealed class UpdateCommand : Command<UpdateSettings>
{
    public override int Execute(CommandContext context, UpdateSettings settings)
    {
        Console.WriteLine($"Updating expense with ID: {settings.Id}");
        Console.WriteLine($"Description: {settings.Description}");
        Console.WriteLine($"Amount: {settings.Amount}");

        return 0;
    }
}
