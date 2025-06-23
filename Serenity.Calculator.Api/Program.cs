using Serenity.Calculator.Api.Extensions;
using Serenity.Calculator.Application;
using Serenity.Calculator.Infrastructure.Database;
using Serenity.Calculator.Infrastructure.Http;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplication();
services.AddHttpInfrastructure(configuration);
services.AddApi();
services.AddDb(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(corsPolicyBuilder =>
        corsPolicyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}

app.UseCustomErrorHandlerMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();