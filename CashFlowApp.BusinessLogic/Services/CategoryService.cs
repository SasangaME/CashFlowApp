using AutoMapper;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Repositories.Repos;
using Microsoft.Extensions.Logging;

namespace CashFlowApp.BusinessLogic.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> FindAll();
    Task<CategoryDto> FindById(int id);
    Task<CategoryDto> Create(CategoryDto request);
    Task<CategoryDto> Update(CategoryDto request);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> FindAll()
    {
        var categories = await _categoryRepository.FindAll();
        _logger.LogInformation("categories are retrieved");
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public Task<CategoryDto> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> Create(CategoryDto request)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> Update(CategoryDto request)
    {
        throw new NotImplementedException();
    }
}