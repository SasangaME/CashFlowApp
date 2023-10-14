using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    using AutoMapper;
    using CashFlowApp.BusinessLogic.Services;
    using CashFlowApp.Models.DTOs;
    using CashFlowApp.Models.Entities;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.FindAll();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _userService.FindById(id);
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserRequest request)
        {
            var response = await _userService.Create(_mapper.Map<User>(request));
            return Ok(_mapper.Map<UserDto>(response));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UserRequest request)
        {
            var response = await _userService.Update(id, _mapper.Map<User>(request));
            return Ok(_mapper.Map<UserDto>(response));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}