using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serenity.Calculator.Application.Common.Interfaces;

namespace Serenity.Calculator.Infrastructure.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("db");

        services.AddDbContext<CalculatorDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IExpressionRepository, ExpressionRepository>();
        return services;
    }
}