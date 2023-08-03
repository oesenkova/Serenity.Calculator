using Serenity.Calculator.Domain.Enums;

namespace Serenity.Calculator.Domain.Entities.Expressions;

public class Expression
{
    public string Definition { get; private set; }

    public string? Result { get; private set; }

    public CalculationProviderType ProviderType { get; private set; }

    public CalculationStatus Status { get; private set; }

    public string? ErrorMessage { get; private set; }

    public Expression(string definition, CalculationProviderType providerType)
    {
        Definition = definition;
        ProviderType = providerType;
    }

    public void SetResult(string result)
    {
        Result = result;
        Status = CalculationStatus.Completed;
    }

    public void SetError(string message)
    {
        Status = CalculationStatus.Failed;
        ErrorMessage = message;
    }
}