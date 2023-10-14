namespace CashFlowApp.Repositories.Repos;

using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> FindAll();
    Task<Role?> FindById(int id);
    Task<int> Create(Role role);
    Task Update(Role role);
}

public class RoleRepository : IRoleRepository
{
    private readonly CashFlowContext _cashFlowContext;

    public RoleRepository(CashFlowContext cashFlowContext)
    {
        _cashFlowContext = cashFlowContext;
    }

    public async Task<IEnumerable<Role>> FindAll()
    {
        return await _cashFlowContext.Roles.ToListAsync();
    }

    public async Task<Role?> FindById(int id)
    {
        return await _cashFlowContext.Roles.FindAsync(id);
    }

    public async Task<int> Create(Role role)
    {
        await _cashFlowContext.Roles.AddAsync(role);
        await _cashFlowContext.SaveChangesAsync();
        return role.Id;
    }

    public async Task Update(Role role)
    {
        _cashFlowContext.Entry(role).State = EntityState.Modified;
        await _cashFlowContext.SaveChangesAsync();
    }
}