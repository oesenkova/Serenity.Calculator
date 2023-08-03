using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Common.Interfaces;

public interface ICalculationProvider
{
    Task<string> CalculateAsync(Expression expression);
}