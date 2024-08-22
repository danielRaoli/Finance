using Finance.API.Application.Requests;
using Finance.API.Application.Services;
using Finance.API.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Finance.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody]AddTransactionRequest request, [FromServices]IValidator<AddTransactionRequest> validator)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userid.Value);
            request.Validate(validator);
            var response = await _transactionService.AddTransaction(request);
            return Created("",response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {

            var autorizeHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine(autorizeHeader);
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
          
            var request = new AddTransactionRequest { UserId = Guid.Parse(userid.Value) };
          
            var response = await _transactionService.GetTransactions(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("greaterthan")]
        public async Task<IActionResult> GetTransactionsGreaterThan([FromQuery] decimal value)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new GetTransactionsGreaterThan { UserId = Guid.Parse(userid!.Value), Value = value };
            var response = await _transactionService.GetAllGreaterThan(request);

            return Ok(response);
        }


        [Authorize]
        [HttpGet("type")]
        public async Task<IActionResult> GetTransactionsByType([FromQuery] TransactionType type)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new GetTransactionByType { UserId = Guid.Parse(userid!.Value), Type = type };
            var response = await _transactionService.GetAllByType(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{transactionid}")]
        public async Task<IActionResult> UpdateTransaction([FromBody]UpdateTransactionRequest request, [FromRoute] Guid transactionid)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.TransactionId = transactionid;
            request.UserId = Guid.Parse(userid.Value);

            var response = await _transactionService.UpdateTransaction(request);  

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{transactionid}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute]Guid transactionid)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new DeleteTransactionRequest { TransactionId = transactionid, UserId = Guid.Parse(userid.Value) };
            var response = await _transactionService.DeleteTransaction(request);

            return Ok(response);
        }
    }
}
