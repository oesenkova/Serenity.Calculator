using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serenity.Calculator.Application.Common.ExternalProviders;
using Serenity.Calculator.Infrastructure.Http.MathJs;

namespace Serenity.Calculator.Infrastructure.Http;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mathJsUri = configuration.GetConnectionString("MathJs");
        ArgumentException.ThrowIfNullOrEmpty(mathJsUri);
        services.AddSingleton(new MathJsConfiguration()
        {
            Uri = mathJsUri
        });
        services.AddScoped<IMathJsProvider, MathJsProvider>();
        return services;
    }
}