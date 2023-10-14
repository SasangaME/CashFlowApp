using AutoMapper;
using CashFlowApp.BusinessLogic.Exceptions;
using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Repos;
using Microsoft.Extensions.Logging;

namespace CashFlowApp.BusinessLogic.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> FindAll();
    Task<Category> FindById(int id);
    Task<Category> Create(Category category);
    Task<Category> Update(int id, Category request);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> FindAll()
    {
        var categories = await _categoryRepository.FindAll();
        return categories;
    }

    public async Task<Category> FindById(int id)
    {
        var category = await _categoryRepository.FindById(id);
        return category ?? throw new NotFoundException($"category not found for id: {id}");
    }

    public async Task<Category> Create(Category category)
    {
        category.CreatedAt = DateTime.Now;
        category.CreatedBy = -1;
        await _categoryRepository.Create(category);
        return category;
    }

    public async Task<Category> Update(int id, Category category)
    {
        var existingCategory = await FindById(id);
        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        existingCategory.UpdatedAt = DateTime.Now;
        existingCategory.UpdatedBy = -1;

        await _categoryRepository.Update(existingCategory);
        return category;
    }
}