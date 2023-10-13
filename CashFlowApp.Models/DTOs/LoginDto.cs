namespace CashFlowApp.Models.DTOs;

using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required(ErrorMessage = "username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "password is required")]
    public string Password { get; set; } = string.Empty;
}