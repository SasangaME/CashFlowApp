namespace CashFlowApp.BusinessLogic.Tests.Services;

using AutoMapper;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.Entities;
using CashFlowApp.Models.Mappings;
using CashFlowApp.Repositories.Repos;
using Moq;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepository = new();

    private IEnumerable<Category> GetCategories()
    {
        List<Category> categories = new();
        categories.Add(new Category() { Id = 1, Name = "Category 1", Description = "Test 1" });
        categories.Add(new Category() { Id = 2, Name = "Category 2", Description = "Test 2" });
        return categories;
    }

    [Fact]
    public async Task GetAllCategories_ReturnsCategoryList()
    {
        var data = GetCategories();
        int pageNumber = 1;
        int pageSize = 10;
        _mockRepository.Setup(m => m.FindAll(pageNumber, pageSize)).ReturnsAsync(data);
        ICategoryService categoryService = new CategoryService(_mockRepository.Object);

        var result = await categoryService.FindAll(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCategory_ReturnsCategory()
    {
        var id = 1;
        var data = GetCategories().FirstOrDefault(s => s.Id == id);
        _mockRepository.Setup(m => m.FindById(id)).ReturnsAsync(data);
        ICategoryService categoryService = new CategoryService(_mockRepository.Object);

        var result = await categoryService.FindById(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task CreateCategory_ReturnsCategory()
    {
        var data = GetCategories().FirstOrDefault();
        var id = 1;
        _mockRepository.Setup(repo => repo.Create(data)).ReturnsAsync(id);
        ICategoryService categoryService = new CategoryService(_mockRepository.Object);

        var result = await categoryService.Create(data);

        Assert.NotNull(result);
        Assert.Equal(data.Name, result.Name);
    }

    [Fact]
    public async Task UpadteCategory_ReturnsCategory()
    {
        var data = GetCategories().FirstOrDefault();
        var id = 1;
        _mockRepository.Setup(m => m.FindById(id)).ReturnsAsync(data);
        _mockRepository.Setup(repo => repo.Update(data));
        ICategoryService categoryService = new CategoryService(_mockRepository.Object);

        var result = await categoryService.Update(id, data);

        Assert.NotNull(result);
        Assert.Equal(data.Name, result.Name);
    }
}