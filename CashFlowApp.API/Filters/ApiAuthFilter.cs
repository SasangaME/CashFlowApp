namespace CashFlowApp.API.Filters;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Utils.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

public class ApiAuthFilter : Attribute, IAsyncActionFilter
{
    private readonly int[] _roles;

    public ApiAuthFilter(params int[] roles)
    {
        _roles = roles;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string? authorizeHeader = context.HttpContext.Request.Headers["Authorization"];
        const string errorMessage = "user not authorized to perform this action";
        if (authorizeHeader.IsNullOrEmpty())
        {
            throw new UnauthorizedException(errorMessage);
        }

        string? token = authorizeHeader?.Replace("Bearer ", "");
        var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var key = config?.GetValue<string>("Jwt:Secret");

        var username = JwtUtil.ValidateToken(token, key);
        if (username.IsNullOrEmpty())
        {
            throw new UnauthorizedException(errorMessage);
        }

        await next();
    }
}