namespace CashFlowApp.Repositories.Repos;

using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    Task<IEnumerable<User>> FindAll();
    Task<User?> FindById(int id);
    Task<int> Create(User user);
    Task Update(User user);
    Task<User?> FindByUsername(string username);
}

public class UserRepository : IUserRepository
{
    private readonly CashFlowContext _cashFlowContext;

    public UserRepository(CashFlowContext cashFlowContext)
    {
        _cashFlowContext = cashFlowContext;
    }

    public async Task<IEnumerable<User>> FindAll()
    {
        return await _cashFlowContext.Users.ToListAsync();
    }

    public async Task<User?> FindById(int id)
    {
        return await _cashFlowContext.Users
            .Include(q => q.Role)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<int> Create(User user)
    {
        await _cashFlowContext.Users.AddAsync(user);
        await _cashFlowContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task Update(User user)
    {
        _cashFlowContext.Entry(user).State = EntityState.Modified;
        await _cashFlowContext.SaveChangesAsync();
    }

    public async Task<User?> FindByUsername(string username)
    {
        return await _cashFlowContext.Users.Include(i => i.Role)
            .FirstOrDefaultAsync(q => q.Username.Equals(username));
    }
}