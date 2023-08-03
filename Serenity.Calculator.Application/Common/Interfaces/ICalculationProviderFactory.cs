using Serenity.Calculator.Domain.Enums;

namespace Serenity.Calculator.Application.Common.Interfaces;

public interface ICalculationProviderFactory
{
    ICalculationProvider GetProvider(CalculationProviderType providerType);
}