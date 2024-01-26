using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApp.Repositories.Repos;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> FindAll(int userId, int pageNumber, int pageSize);
    Task<Transaction> FindById(int id, int userId);
    Task<int> Create(Transaction transaction);
    Task Update(Transaction taTransaction);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly CashFlowContext _dbContext;

    public TransactionRepository(CashFlowContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Transaction>> FindAll(int userId, int pageNumber, int pageSize)
    {
        return await _dbContext.Transactions
            .Where(q => q.CreatedBy == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Transaction> FindById(int id, int userId)
    {
        return await _dbContext.Transactions
            .SingleAsync(q => q.Id == id && q.CreatedBy == userId);
    }

    public async Task<int> Create(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();
        return transaction.Id;
    }

    public async Task Update(Transaction taTransaction)
    {
        _dbContext.Entry(taTransaction).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}