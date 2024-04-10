using AutoMapper;
using CashFlowApp.API.Filters;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using CashFlowApp.Models.Enums;
using CashFlowApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize(RoleEnum.User)]
    public class TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService,
        IMapper mapper, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly int _userId = AuthUtil.GetUserIdFromContext(httpContextAccessor.HttpContext);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var transactions = await transactionService.FindAll(_userId, pageNumber, pageSize);
            logger.LogInformation($"transactions retrieved for user: {_userId}");
            return Ok(mapper.Map<IEnumerable<TransactionDto>>(transactions));
        }

        [HttpGet("id")]
        public async Task<ActionResult<TransactionDto>> Get(int id)
        {
            var transaction = await transactionService.FindById(id, _userId);
            logger.LogInformation($"Transaction : {id} retrieved for user: {_userId}");
            return Ok(mapper.Map<TransactionDto>(transaction));
        }

    }
}