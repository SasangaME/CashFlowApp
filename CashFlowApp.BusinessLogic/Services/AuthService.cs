using Microsoft.AspNetCore.Http;

namespace CashFlowApp.BusinessLogic.Services;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using CashFlowApp.Models.Enums;
using CashFlowApp.Utils.Security;
using Microsoft.Extensions.Configuration;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<bool> ValidateUserRole(string username, RoleEnum[] roles);
    int GetUserIdFromContext(HttpContext context);
}

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userService.FindByUsername(request.Username);
        var isAuthenticate = AuthenticateUser(user, request);
        if (!isAuthenticate)
        {
            throw new UnauthorizedException("incorrect password");
        }
#nullable disable
        var token = JwtUtil.CreateToken(
            key: _configuration["Jwt:Secret"],
            username: user.Username,
            userId: user.Id,
            issuer: _configuration["Jwt:ValidIssuer"],
            audience: _configuration["Jwt:ValidAudience"]
        );
#nullable enable
        return new LoginResponse { Token = token };
    }

    public async Task<bool> ValidateUserRole(string username, RoleEnum[] roles)
    {
        var user = await _userService.FindByUsername(username)
                   ?? throw new UnauthorizedException("user not found");

#pragma warning disable CS8602
        var userRole = user.Role.RoleEnum;
        if (userRole == RoleEnum.Admin)
            return true;

        var isAuthorized = false;
        foreach (var role in roles)
        {
            if (role == userRole)
            {
                isAuthorized = true;
                break;
            }
        }

        return isAuthorized;
    }

    private bool AuthenticateUser(User user, LoginRequest request)
    {
        var passwordHash = PasswordHash.HashPassword(request.Password);
        return user.Password.Equals(passwordHash);
    }

    public int GetUserIdFromContext(HttpContext context)
    {
        var userId = context.Items["UserId"] ?? throw new ValidationException("user id not found");
        return Convert.ToInt32(userId);
    }
}