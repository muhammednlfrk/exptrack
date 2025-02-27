using ExpenseTracker.CLI.Commands;
using ExpenseTracker.CLI.Infrastructure;
using ExpenseTracker.Core;
using ExpenseTracker.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

// Create db directory if not exists
string dbDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "exptrack");
if (!Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
    AnsiConsole.MarkupLine($"[bold green]Database directory created at {dbDirectory}[/]");
}

// Create a new service collection
ServiceCollection services = new();
services.AddSingleton<IExpenseRepository>(p => new SqLiteExpenseTrackerRepository(
    sqliteFilePath: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "exptrack", "expenses.db")
));
services.AddSingleton<IExpenseService, ExpenseService>();

// Create a new command application
ServiceCollectionRegistrar registrar = new(services);
CommandApp app = new(registrar);

// Configure the application
app.Configure(config =>
{
#if DEBUG
    config.ValidateExamples();
#endif

    config.SetApplicationName("exptrack")
        .SetApplicationVersion("exptrack v1.0.0")
        .UseStrictParsing();

    config.AddCommand<AddCommand>("add")
        .WithDescription("Add a new expense to the database.")
        .WithExample(["add", "--amount", "100", "--description", "Lunch"])
        .WithAlias("ad");

    config.AddCommand<ListCommand>("list")
        .WithDescription("List all expenses.")
        .WithExample(["list"])
        .WithExample(["list", "--id", "12"])
        .WithExample(["list", "--month", "7"])
        .WithAlias("ls");

    config.AddCommand<UpdateCommand>("update")
        .WithDescription("Update an expense.")
        .WithExample(["update", "12", "--amount", "200"])
        .WithExample(["update", "12", "--description", "Dinner"])
        .WithAlias("up");

    config.AddCommand<DeleteCommand>("delete")
        .WithDescription("Delete an expense.")
        .WithExample(["delete", "12"])
        .WithExample(["delete", "12", "--force"])
        .WithAlias("del")
        .WithAlias("remove")
        .WithAlias("rm");

    config.AddCommand<SummaryCommand>("summary")
        .WithDescription("Show a summary of expenses.")
        .WithExample(["summary"])
        .WithExample(["summary", "--month", "7"])
        .WithExample(["summary", "--year", "2021"])
        .WithExample(["summary", "--month", "7", "--year", "2021"])
        .WithAlias("sum");
});

// Run the application
return app.Run(args);