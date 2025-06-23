using Microsoft.EntityFrameworkCore;
using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Infrastructure.Database;

public class CalculatorDbContext : DbContext
{
    public CalculatorDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Expression> Expressions { get; set; } = null!;
}