using AutoMapper;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using CashFlowApp.Repositories.Repos;
using Microsoft.Extensions.Logging;

namespace CashFlowApp.BusinessLogic.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> FindAll();
    Task<CategoryDto> FindById(int id);
    Task<CategoryDto> Create(CategoryDto request);
    Task<CategoryDto> Update(int id, CategoryDto request);
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

    public async Task<CategoryDto> FindById(int id)
    {
        var category = await _categoryRepository.FindById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> Create(CategoryDto request)
    {
        Category category = _mapper.Map<Category>(request);
        int id = await _categoryRepository.Create(category);
        request.Id = id;
        return request;
    }

    public async Task<CategoryDto> Update(int id, CategoryDto request)
    {
        Category category = _mapper.Map<Category>(request);
        await _categoryRepository.Update(category);
        return request;
    }
}