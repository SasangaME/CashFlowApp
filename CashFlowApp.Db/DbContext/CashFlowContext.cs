using CashFlowApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApp.Repositories.Db;

public class CashFlowContext(DbContextOptions<CashFlowContext> options) : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveAuditData();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SaveAuditData()
    {
        var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity
            && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedAt = DateTime.Now;
            }
            else
            {
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.Now;
            }
        }
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}