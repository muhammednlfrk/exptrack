using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ExpenseTracker.CLI.Infrastructure;

public sealed class ServiceCollectionRegistrar([NotNull] IServiceCollection _serviceCollection) : ITypeRegistrar
{
    public void Register(Type service, Type implementation)
    {
        _serviceCollection.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _serviceCollection.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        _serviceCollection.AddSingleton(service, factory);
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(_serviceCollection.BuildServiceProvider());
    }

    private sealed class TypeResolver([NotNull] IServiceProvider _serviceProvider) : ITypeResolver, IDisposable
    {
        public object? Resolve(Type? type)
        {
            if (type is null) return null;
            return _serviceProvider.GetRequiredService(type);
        }

        public void Dispose()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}

