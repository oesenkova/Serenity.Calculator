using Serenity.Calculator.Application.Common.LocalProviders.Local;
using Serenity.Calculator.Domain.Entities.Expressions;
using Serenity.Calculator.Domain.Enums;
using Xunit;

namespace Serenity.Calculator.Application.Tests;

public class LocalProviderTests
{
    private readonly ILocalExpressionProvider _provider;

    public LocalProviderTests()
    {
        _provider = new LocalExpressionProvider();
    }

    [Fact]
    public async Task Math_Operator_Add_Test()
    {
        Expression expr = new Expression("1+3.5", CalculationProviderType.Local);
        Assert.Equal("4.5", await _provider.CalculateAsync(expr));
    }
    
    [Fact]
    public async Task Math_Operator_Subtract_Test()
    {
        Expression expr = new Expression("1-3.5", CalculationProviderType.Local);
        Assert.Equal("-2.5", await _provider.CalculateAsync(expr));
    }
    
    [Fact]
    public async Task Math_Operator_Mul_Test()
    {
        Expression expr = new Expression("1.5*1.5", CalculationProviderType.Local);
        Assert.Equal("2.25", await _provider.CalculateAsync(expr));
    }
    
    [Fact]
    public async Task Math_Operator_Div_Test()
    {
        Expression expr = new Expression("5/2", CalculationProviderType.Local);
        Assert.Equal("2.5", await _provider.CalculateAsync(expr));
    }
    
    [Fact]
    public async Task Math_WithFirstMinus_Test()
    {
        Expression expr = new Expression("-5-2", CalculationProviderType.Local);
        Assert.Equal("-7", await _provider.CalculateAsync(expr));
    }
    
    [Fact]
    public async Task Math_WithBrackets_Test()
    {
        Expression expr = new Expression("-5*(-2-7)", CalculationProviderType.Local);
        Assert.Equal("45", await _provider.CalculateAsync(expr));
    }
}