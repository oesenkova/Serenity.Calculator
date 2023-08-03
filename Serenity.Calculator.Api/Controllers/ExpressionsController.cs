using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serenity.Calculator.Application.Expressions.Commands.CalculateExpression;
using Serenity.Calculator.Application.Expressions.Commands.CalculateExpression.Models;

namespace Serenity.Calculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ExpressionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpressionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(CalculateExpressionResultDto),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CalculateExpressionResultDto>> Calculate([FromBody] CalculateExpressionDto payload)
    {
        var expressionResult = await _mediator.Send(new CalculateExpressionCommand(payload));

        return Ok(expressionResult);
    }
}