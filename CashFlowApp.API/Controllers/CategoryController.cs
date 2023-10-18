using AutoMapper;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    using CashFlowApp.API.Filters;
    using CashFlowApp.Models.Constants;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Category
        [HttpGet]
        // [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var categories = await _categoryService.FindAll(pageNumber, pageSize);
            _logger.LogInformation("category list retrieved");
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [ApiAuthorize(UserRoles.Admin)]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.FindById(id);
            _logger.LogInformation($"category retrieved for id: {id}");
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Post([FromBody] CategoryDto request)
        {
            var response = await _categoryService.Create(_mapper.Map<Category>(request));
            _logger.LogInformation($"new category created for id: {response.Id}");
            return Ok(_mapper.Map<CategoryDto>(response));
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Put(int id, [FromBody] CategoryDto request)
        {
            var response = await _categoryService.Update(id, _mapper.Map<Category>(request));
            _logger.LogInformation($"category updated for id: {id}");
            return Ok(_mapper.Map<CategoryDto>(response));
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}