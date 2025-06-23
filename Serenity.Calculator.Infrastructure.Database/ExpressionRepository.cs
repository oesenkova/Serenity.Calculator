using Microsoft.EntityFrameworkCore;
using Serenity.Calculator.Application.Common.Interfaces;
using Serenity.Calculator.Domain.Entities.Expressions;
using Serenity.Calculator.Domain.Exceptions;

namespace Serenity.Calculator.Infrastructure.Database;

public class ExpressionRepository : IExpressionRepository
{
    private readonly CalculatorDbContext _context;

    public ExpressionRepository(CalculatorDbContext context)
    {
        _context = context;
    }

    public async Task<Expression> GetAsync(int id)
    {
        var expression = await _context.Expressions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (expression == null) throw new NotFoundException("Expression not found.");
        
        return expression;
    }

    public Task<List<Expression>> GetAsync()
    {
        return _context.Expressions.AsNoTracking().ToListAsync();
    }

    public async Task<Expression> UpdateAsync(int id, Expression expressionToUpdate)
    {
        expressionToUpdate.Id = id;
        _context.Expressions.Update(expressionToUpdate);
        await _context.SaveChangesAsync();

        return expressionToUpdate;
    }

    public async Task<Expression> CreateAsync(Expression expressionToCreate)
    {
        _context.Expressions.Add(expressionToCreate);
        await _context.SaveChangesAsync();

        return expressionToCreate;
    }

    public async Task DeleteAsync(int id)
    {
        var expression = await _context.Expressions.FindAsync(id);

        if (expression != null) _context.Expressions.Remove(expression);
        await _context.SaveChangesAsync();
    }
}