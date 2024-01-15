namespace CashFlowApp.Models.Entities;

public class Transaction : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsDeducting { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}