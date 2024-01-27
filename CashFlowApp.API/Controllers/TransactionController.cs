using AutoMapper;
using CashFlowApp.API.Filters;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;
using CashFlowApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize(RoleEnum.User)]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly int _userId;
        private readonly IMapper _mapper;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService, IAuthService authService, IMapper mapper)
        {
            _logger = logger;
            _transactionService = transactionService;
            _userId = authService.GetUserIdFromContext(this.HttpContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var transactions = await _transactionService.FindAll(_userId, pageNumber, pageSize);
            _logger.LogInformation($"transactions retrieved for user: {_userId}");
            return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactions));
        }

        // [HttpPost]
        // public async Task<ActionResult<TransactionDto>> Get(int id)
        // {
        //     
        // }

    }
}