using System.Net;
using System.Text.Json;
using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.DTOs;

namespace CashFlowApp.API.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await SendResponse(context, e, e switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                ValidationException _ => HttpStatusCode.BadRequest,
                UnauthorizedException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            });
        }
    }

    private async Task SendResponse(HttpContext context, Exception e, HttpStatusCode statusCode)
    {
        _logger.LogError(e.Message, e);
        ErrorResponse err = new()
        {
            Message = e.Message,
            StackTrace = e.StackTrace,
        };
        var response = JsonSerializer.Serialize(err);
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(response);
    }
}