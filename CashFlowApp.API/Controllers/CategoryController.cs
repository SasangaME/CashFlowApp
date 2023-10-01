using AutoMapper;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _categoryService.FindAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.FindById(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Post([FromBody] CategoryDto request)
        {
            var response = await _categoryService.Create(_mapper.Map<Category>(request));
            return Ok(_mapper.Map<CategoryDto>(response));
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Put(int id, [FromBody] CategoryDto request)
        {
            var response = await _categoryService.Update(id, _mapper.Map<Category>(request));
            return Ok(_mapper.Map<CategoryDto>(response));
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}