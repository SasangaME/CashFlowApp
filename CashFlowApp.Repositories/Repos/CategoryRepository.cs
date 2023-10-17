using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApp.Repositories.Repos;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> FindAll(int pageNumber, int pageSize);
    Task<Category?> FindById(int id);
    Task<int> Create(Category category);
    Task Update(Category category);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly CashFlowContext _dbContext;

    public CategoryRepository(CashFlowContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> FindAll(int pageNumber, int pageSize)
    {
        return await _dbContext.Categories
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Category?> FindById(int id)
    {
        return await _dbContext.Categories.FindAsync(id);
    }

    public async Task<int> Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return category.Id;
    }

    public async Task Update(Category category)
    {
        _dbContext.Entry(category).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}