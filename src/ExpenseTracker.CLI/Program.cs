using ExpenseTracker.CLI.Commands;
using ExpenseTracker.CLI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

// Create a new service collection
ServiceCollection services = new();

// Create a new command application
ServiceCollectionRegistrar registrar = new(services);
CommandApp app = new(registrar);

// Configure the application
app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
#endif

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