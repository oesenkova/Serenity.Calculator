using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serenity.Calculator.Domain.Exceptions;

namespace Serenity.Calculator.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _factory;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ProblemDetailsFactory factory, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _factory = factory;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment env)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while executing request : {}. Message : {}",  context.Request.Path, ex.Message);

            var problemDetails = ex switch
            {
                NotFoundException =>
                    _factory.CreateProblemDetails(context, StatusCodes.Status404NotFound, detail: ex.Message),
                CalculationException =>
                    _factory.CreateProblemDetails(context, StatusCodes.Status409Conflict, detail: ex.Message),
                ValidationException =>
                    _factory.CreateProblemDetails(context, StatusCodes.Status400BadRequest, detail: ex.Message),
                _ => _factory.CreateProblemDetails(context)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status!.Value;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}