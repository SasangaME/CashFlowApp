namespace CashFlowApp.BusinessLogic.Tests;

using AutoMapper;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.Entities;
using CashFlowApp.Models.Mappings;
using CashFlowApp.Repositories.Repos;
using Moq;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepository;
    private readonly IMapper _mapper;

    public CategoryServiceTests()
    {
        _mockRepository = new Mock<ICategoryRepository>();
        var mapperConfig = new MapperConfiguration(config
            =>
        {
            config.AddProfile(new ApplicationProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    private IEnumerable<Category> GetCategories()
    {
        List<Category> categories = new();
        categories.Add(new Category() { Id = 1, Name = "Category 1", Description = "Test 1" });
        categories.Add(new Category() { Id = 2, Name = "Category 2", Description = "Test 2" });
        return categories;
    }

    [Fact]
    public async Task GetAllCategories_ReturnCategoryList()
    {
        var data = GetCategories();
        _mockRepository.Setup(m => m.FindAll()).ReturnsAsync(data);
        ICategoryService categoryService = new CategoryService(_mockRepository.Object, _mapper);
        
        var result = await categoryService.FindAll();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}