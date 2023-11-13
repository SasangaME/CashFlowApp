namespace CashFlowApp.API.Filters;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Utils.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

public class ApiAuthorizationScope : Attribute, IAsyncAuthorizationFilter
{
    private readonly int[] _roles;

    public ApiAuthorizationScope(params int[] roles)
    {
        _roles = roles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        const string errorMessage = "user not authorized to perform this action";
        if (context == null)
            throw new UnauthorizedException(errorMessage);

        string? authorizeHeader = context.HttpContext.Request.Headers["Authorization"];

        string? token = authorizeHeader?.Replace("Bearer ", "");
        var config = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var key = config?.GetValue<string>("Jwt:Secret");

        var username = JwtUtil.ValidateToken(token, key);
        if (username.IsNullOrEmpty())
        {
            throw new UnauthorizedException(errorMessage);
        }

        var userService = context.HttpContext.RequestServices.GetService<IAuthService>();
        var hasRole = await userService.ValidateUserRole(username, _roles);

        if (!hasRole)
            throw new UnauthorizedException(errorMessage);
    }
}