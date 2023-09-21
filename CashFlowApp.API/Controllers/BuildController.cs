using CashFlowApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildController : ControllerBase
{
    [HttpGet]
    public IActionResult Build()
    {
        var build = new BuildDto { Version = "1.0.0", BuildNumber = "2023092201"};
        return Ok(build);
    }
}