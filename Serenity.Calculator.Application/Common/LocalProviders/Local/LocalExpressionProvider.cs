using System.Globalization;
using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Common.LocalProviders.Local;

public class LocalExpressionProvider : ILocalExpressionProvider
{
    public Task<string> CalculateAsync(Expression expression)
    {
        return Task.FromResult(Eval(expression.Definition).ToString());
    }

    private double Eval(string exp)
    {
        if (exp.StartsWith('-') && exp.TrimStart('-').All(x => char.IsDigit(x) || x == '.'))
        {
            return double.Parse(exp, CultureInfo.InvariantCulture);
        }
        var bracketCounter = 0;
        var operatorIndex = -1;

        for (int i = 0; i < exp.Length; i++)
        {
            var c = exp[i];
            if (c == '(') bracketCounter++;
            else if (c == ')') bracketCounter--;
            else if (c is '+' or '-' && bracketCounter == 0 && i != 0)
            {
                operatorIndex = i;
                break;
            }
            else if (c is '*' or '/' && bracketCounter == 0 && operatorIndex < 0)
            {
                operatorIndex = i;
            }
        }

        if (operatorIndex < 0)
        {
            exp = exp.Trim();
            if (exp[0] == '(' && exp[^1] == ')')
                return Eval(exp.Substring(1, exp.Length - 2));
            return double.Parse(exp, CultureInfo.InvariantCulture);
        }

        return exp[operatorIndex] switch
        {
            '+' => Eval(exp[..operatorIndex]) + Eval(exp[(operatorIndex + 1)..]),
            '-' => Eval(exp[..operatorIndex]) - Eval(exp[(operatorIndex + 1)..]),
            '*' => Eval(exp[..operatorIndex]) * Eval(exp[(operatorIndex + 1)..]),
            '/' => Eval(exp[..operatorIndex]) / Eval(exp[(operatorIndex + 1)..]),
            _ => 0
        };
    }
}