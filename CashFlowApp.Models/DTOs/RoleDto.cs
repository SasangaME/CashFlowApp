using CashFlowApp.Models.Enums;

namespace CashFlowApp.Models.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public RoleEnum RoleEnum { get; set; }
}