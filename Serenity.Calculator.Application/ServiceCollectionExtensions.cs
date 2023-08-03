using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serenity.Calculator.Application.Common;
using Serenity.Calculator.Application.Common.Behaviors;
using Serenity.Calculator.Application.Common.Interfaces;
using Serenity.Calculator.Application.Common.LocalProviders;
using Serenity.Calculator.Application.Common.LocalProviders.Local;
using Serenity.Calculator.Application.Common.LocalProviders.MathEval;

namespace Serenity.Calculator.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ICalculationProviderFactory, CalculationProviderFactory>();
        services.AddScoped<IMathEvalProvider, MathEvalProvider>();
        services.AddScoped<ILocalExpressionProvider, LocalExpressionProvider>();
        return services;
    }
}