namespace CashFlowApp.Models.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }
}