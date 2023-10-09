using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    using CashFlowApp.BusinessLogic.Services;
    using CashFlowApp.Models.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/Todos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET: api/Todos/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<TodoDto>> Get(int id)
        {
            var response = await _todoService.GetTodo(id);
            return Ok(response);
        }

        // POST: api/Todos
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Todos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Todos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}