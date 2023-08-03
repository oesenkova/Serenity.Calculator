using AutoMapper;
using Serenity.Calculator.Application.Expressions.Commands.CalculateExpression.Models;
using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Expressions;

public class ExpressionsProfile : Profile
{
    public ExpressionsProfile()
    {
        CreateMap<Expression, CalculateExpressionResultDto>();
    }
    
}