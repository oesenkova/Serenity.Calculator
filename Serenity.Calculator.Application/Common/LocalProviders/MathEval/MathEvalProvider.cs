using Serenity.Calculator.Domain.Entities.Expressions;
using Serenity.Calculator.Domain.Exceptions;
using EvalExpression = org.matheval.Expression;

namespace Serenity.Calculator.Application.Common.LocalProviders.MathEval;

public class MathEvalProvider : IMathEvalProvider
{
    public Task<string> CalculateAsync(Expression expression)
    {
        var evalExpression = new EvalExpression(expression.Definition);

        var errors = evalExpression.GetError();
        if (errors.Any())
        {
            throw new CalculationException(string.Join(".", errors));
        }
        
        var result = evalExpression.Eval<string>();
        return Task.FromResult(result);
    }
}