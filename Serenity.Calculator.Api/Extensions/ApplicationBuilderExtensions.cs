using Serenity.Calculator.Api.Middlewares;

namespace Serenity.Calculator.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomErrorHandlerMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<ErrorHandlerMiddleware>();
}