namespace CashFlowApp.Models.DTOs;

using CashFlowApp.Models.Entities;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int RoleId { get; set; }
    public Role? Role { get; set; }
}