using AutoMapper;
using Serenity.Calculator.Application.Expressions;
using Xunit;

namespace Serenity.Calculator.Application.Tests;

public class MappingTests
{
    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ExpressionsProfile>();
        });

        configuration.AssertConfigurationIsValid();
    }
}