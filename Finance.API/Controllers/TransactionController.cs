using Finance.API.Application.Requests;
using Finance.API.Application.Services;
using Finance.API.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody]AddTransactionRequest request)
        {
            var response = await _transactionService.AddTransaction(request);
            return Created("",response);
        }
        [HttpGet()]
        public async Task<IActionResult> GetTransactions()
        {
            var request = new AddTransactionRequest {UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6") };
            var response = await _transactionService.GetTransactions(request);

            return Ok(response);
        }

        [HttpGet("/greaterthan")]
        public async Task<IActionResult> GetTransactionsGreaterThan([FromQuery] decimal value)
        {
            var request = new GetTransactionsGreaterThan { UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Value = value };
            var response = await _transactionService.GetAllGreaterThan(request);

            return Ok(response);
        }

        [HttpGet("/type")]
        public async Task<IActionResult> GetTransactionsByType([FromQuery] TransactionType type)
        {
            var request = new GetTransactionByType { UserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Type = type };
            var response = await _transactionService.GetAllByType(request);

            return Ok(response);
        }
    }
}
