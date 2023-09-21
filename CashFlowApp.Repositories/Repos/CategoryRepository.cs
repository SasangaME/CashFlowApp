using CashFlowApp.Models.Entities;

namespace CashFlowApp.Repositories.Repos;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> FindAll();
    Task<Category?> FindById(int id);
    Task<int> Create(Category category);
    Task Update(Category category);
}

public class CategoryRepository : ICategoryRepository
{
    public Task<IEnumerable<Category>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<Category?> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Create(Category category)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category category)
    {
        throw new NotImplementedException();
    }
}