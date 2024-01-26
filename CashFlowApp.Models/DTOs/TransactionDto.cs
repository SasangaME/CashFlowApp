using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowApp.Models.Entities;

namespace CashFlowApp.Models.DTOs
{
    public class TransactionDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsOutflow { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = new();
    }
}
