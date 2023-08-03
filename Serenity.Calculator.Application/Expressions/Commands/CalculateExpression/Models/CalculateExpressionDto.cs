using Serenity.Calculator.Domain.Enums;

namespace Serenity.Calculator.Application.Expressions.Commands.CalculateExpression.Models;

public record CalculateExpressionDto(string Definition, CalculationProviderType ProviderType);