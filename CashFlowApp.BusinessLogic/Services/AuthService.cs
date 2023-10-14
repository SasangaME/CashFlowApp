namespace CashFlowApp.BusinessLogic.Services;

using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using CashFlowApp.Utils.Security;
using Microsoft.Extensions.Configuration;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);
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
            throw new UnauthorizedException("incorrect password");

        var token = JwtUtil.CreateToken(
            key: _configuration["Jwt:Secret"],
            username: user.Username,
            role: user.Role.Name,
            issuer: _configuration["Jwt:ValidIssuer"],
            audience: _configuration["Jwt:ValidAudience"],
            name: $"{user.FirstName} {user.LastName}"
        );
        return new LoginResponse { Token = token };
    }

    private bool AuthenticateUser(User user, LoginRequest request)
    {
        var passwordHash = PasswordHash.HashPassword(request.Password);
        return user.Password.Equals(passwordHash);
    }
}