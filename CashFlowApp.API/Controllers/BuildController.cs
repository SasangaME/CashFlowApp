using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildController : ControllerBase
{
    [HttpGet("hello")]
    public IActionResult Hello()
    {
        return Ok("hello world!");
    }
}