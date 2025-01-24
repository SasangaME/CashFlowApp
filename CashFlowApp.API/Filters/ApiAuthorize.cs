namespace CashFlowApp.API.Filters;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.Enums;
using CashFlowApp.Utils.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

[AttributeUsage(AttributeTargets.All)]
public class ApiAuthorize(params RoleEnum[] roles) : Attribute, IAsyncActionFilter
{
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

        var jwtInfo = JwtUtil.ValidateToken(token, key) ?? throw new UnauthorizedException(errorMessage);

        var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
        var hasRole = await authService.ValidateUserRole(jwtInfo.Username, roles);

        if (!hasRole)
            throw new UnauthorizedException(errorMessage);

        context.HttpContext.Items.Add("userId", jwtInfo.UserId);
        await next();
    }
}