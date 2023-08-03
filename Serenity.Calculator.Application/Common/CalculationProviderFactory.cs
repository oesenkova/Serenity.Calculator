using Microsoft.Extensions.DependencyInjection;
using Serenity.Calculator.Application.Common.ExternalProviders;
using Serenity.Calculator.Application.Common.Interfaces;
using Serenity.Calculator.Application.Common.LocalProviders;
using Serenity.Calculator.Application.Common.LocalProviders.Local;
using Serenity.Calculator.Application.Common.LocalProviders.MathEval;
using Serenity.Calculator.Domain.Enums;
using Serenity.Calculator.Domain.Exceptions;

namespace Serenity.Calculator.Application.Common;

public class CalculationProviderFactory : ICalculationProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CalculationProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICalculationProvider GetProvider(CalculationProviderType providerType)
    {
        var instanceType = GetProviderInstanceType(providerType);
        return (ICalculationProvider) _serviceProvider.GetRequiredService(instanceType);
    }

    private Type GetProviderInstanceType(CalculationProviderType providerType)
    {
        return providerType switch
        {
            CalculationProviderType.Local => typeof(ILocalExpressionProvider),
            CalculationProviderType.MathJs => typeof(IMathJsProvider),
            CalculationProviderType.MathEval => typeof(IMathEvalProvider),
            _ => throw new NotFoundException("Provider not found")
        };
    }
}