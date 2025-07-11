﻿using AutoMapper;
using MediatR;
using Serenity.Calculator.Application.Common.Interfaces;
using Serenity.Calculator.Application.Expressions.Commands.CalculateExpression.Models;
using Serenity.Calculator.Domain.Entities.Expressions;

namespace Serenity.Calculator.Application.Expressions.Commands.CalculateExpression;

public record CalculateExpressionCommand(CalculateExpressionDto Payload) : IRequest<CalculateExpressionResultDto>;

public class CalculateExpressionCommandHandler : IRequestHandler<CalculateExpressionCommand, CalculateExpressionResultDto>
{
    private readonly ICalculationProviderFactory _providerFactory;
    private readonly IMapper _mapper;
    private readonly IExpressionRepository _expressionRepository;

    public CalculateExpressionCommandHandler(ICalculationProviderFactory providerFactory, IMapper mapper, IExpressionRepository expressionRepository)
    {
        _providerFactory = providerFactory;
        _mapper = mapper;
        _expressionRepository = expressionRepository;
    }

    public async Task<CalculateExpressionResultDto> Handle(CalculateExpressionCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;

        var definition = payload.Definition;
        var providerType = payload.ProviderType;

        var expression = new Expression(definition, providerType);

        try
        {
            var calculationProvider = _providerFactory.GetProvider(providerType);
            var calculationResult = await calculationProvider.CalculateAsync(expression);
            expression.SetResult(calculationResult.Replace(",", "."));
        }
        catch (Exception e)
        {
            expression.SetError(e.Message);
        }
        finally
        {
            await _expressionRepository.CreateAsync(expression);
        }

        return _mapper.Map<CalculateExpressionResultDto>(expression);
    }
}