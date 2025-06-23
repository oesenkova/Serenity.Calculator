using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Common.Interfaces;

public interface IExpressionRepository
{
    Task<Expression> GetAsync(int id);

    Task<List<Expression>> GetAsync();

    Task<Expression> UpdateAsync(int id, Expression expressionToUpdate);

    Task<Expression> CreateAsync(Expression expressionToCreate);

    Task DeleteAsync(int id);
}