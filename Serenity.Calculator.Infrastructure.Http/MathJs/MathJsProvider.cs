using RestSharp;
using Serenity.Calculator.Application.Common.ExternalProviders;
using Serenity.Calculator.Domain.Entities.Expressions;
using Serenity.Calculator.Domain.Exceptions;

namespace Serenity.Calculator.Infrastructure.Http.MathJs;

public class MathJsProvider : IMathJsProvider
{
    private readonly RestClient _client;

    public MathJsProvider(MathJsConfiguration configuration)
    {
        _client = new RestClient(configuration.Uri);
    }
    
    public async Task<string> CalculateAsync(Expression expression)
    {
        var request = new RestRequest("/");
        request.AddQueryParameter("expr", expression.Definition);
        var result = await _client.ExecuteAsync(request);

        var stringResult = result.Content;

        if (result.IsSuccessStatusCode)
        {
            return stringResult!;
        }

        throw new CalculationException($"API error: {stringResult}");
    }
}