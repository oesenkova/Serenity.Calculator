using Serenity.Calculator.Domain.Enums;

namespace Serenity.Calculator.Application.Expressions.Commands.CalculateExpression.Models;

public record CalculateExpressionResultDto(string Definition, string? Result, CalculationProviderType ProviderType,
    CalculationStatus Status, string? ErrorMessage);