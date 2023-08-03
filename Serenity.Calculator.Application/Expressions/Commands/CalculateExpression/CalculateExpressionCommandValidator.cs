using FluentValidation;
using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Expressions.Commands.CalculateExpression;

public class CalculateExpressionCommandValidator : AbstractValidator<CalculateExpressionCommand>
{
    public CalculateExpressionCommandValidator()
    {
        RuleFor(x => x.Payload).ChildRules(x =>
        {
            x.RuleFor(p => p.ProviderType).IsInEnum();
            x.RuleFor(p => p.Definition).Matches(ExpressionConstants.MathRegularExpression);
        });
    }
}