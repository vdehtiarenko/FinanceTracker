using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<TransactionDto>>> GetListAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                return BadRequest("Invalid date values.");
            }

            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            if (startDate.Date > DateTime.Now.Date)
            {
                return BadRequest("Start date cannot be in the future.");
            }

            if (endDate.Date > DateTime.Now.Date)
            {
                return BadRequest("End date cannot be in the future.");
            }

            var transactions = await _transactionService.GetTransactionsAsync(startDate, endDate);

            if (transactions == null)
            {
                transactions = new List<TransactionDto>();
            }

            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<TransactionDto>> GetByIdAsync(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest("Invalid transaction ID.");
            }

            var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);

            if (transaction is null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }       

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateAsync(CreateTransactionDto createTransactionDto)
        {
            if (createTransactionDto is null)
            {
                return BadRequest("Request body is null.");
            }

            var transaction = await _transactionService.CreateTransactioneAsync(createTransactionDto);

            return Ok(transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            if (updateTransactionDto is null)
            {
                return BadRequest("Request body is null.");
            }

            await _transactionService.UpdateTransactionAsync(updateTransactionDto);

            return NoContent();
        }

        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> DeleteAsync(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest("Invalid transaction ID.");
            }

            await _transactionService.DeleteTransactionAsync(transactionId);

            return NoContent();
        }
    }
}
