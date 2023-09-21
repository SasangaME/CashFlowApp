using CashFlowApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApp.Repositories.Db;

public class CashFlowContext : DbContext
{
    public CashFlowContext(DbContextOptions<CashFlowContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
}