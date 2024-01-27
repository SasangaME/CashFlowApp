using CashFlowApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApp.Models.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public RoleEnum RoleEnum { get; set; }
}