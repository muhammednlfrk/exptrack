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
// #if DEBUG
//     config.PropagateExceptions();
// #endif

    config.AddCommand<AddCommand>("add")
        .WithDescription("Add a new expense")
        .WithAlias("ad");

    config.AddCommand<ListCommand>("list")
        .WithDescription("List all expenses")
        .WithAlias("ls");

    config.AddCommand<UpdateCommand>("update")
        .WithDescription("Update an expense");

    config.AddCommand<DeleteCommand>("delete")
        .WithDescription("Delete an expense")
        .WithAlias("dl")
        .WithAlias("del")
        .WithAlias("remove")
        .WithAlias("rm");

    config.AddCommand<SummaryCommand>("summary")
        .WithDescription("Show a summary of expenses")
        .WithAlias("sm")
        .WithAlias("sum");
});

// Run the application
return app.Run(args);