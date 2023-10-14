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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, IMapper mapper, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _roleService.FindAll();
            _logger.LogInformation("role list retrieved");
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
        }

        // GET: api/Roles/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var role = await _roleService.FindById(id);
            _logger.LogInformation($"role retrieved for id: {id}");
            return Ok(_mapper.Map<RoleDto>(role));
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create([FromBody] RoleDto role)
        {
            var response = await _roleService.Create(_mapper.Map<Role>(role));
            _logger.LogInformation($"role created for id: {response.Id}");
            return Ok(_mapper.Map<RoleDto>(response));
        }

        // PUT: api/Roles/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleDto>> Update(int id, [FromBody] RoleDto role)
        {
            var response = await _roleService.Update(id, _mapper.Map<Role>(role));
            _logger.LogInformation($"role updated for id: {response.Id}");
            return Ok(_mapper.Map<RoleDto>(response));
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}