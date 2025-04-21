using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : Controller
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionTypeDto>>> GetListAsync()
        {
            var transactionsTypes = await _transactionTypeService.GetTransactionTypesAsync();

            if (transactionsTypes == null || !transactionsTypes.Any())
            {
                return NotFound();
            }

            return Ok(transactionsTypes);
        }

        [HttpGet("{transactionTypeId}")]
        public async Task<ActionResult<TransactionTypeDto>> GetByIdAsync(Guid transactionTypeId)
        {
            if (transactionTypeId == Guid.Empty)
            {
                return BadRequest("Invalid transaction type ID.");
            }

            var transactionType = await _transactionTypeService.GetTransactionTypeByIdAsync(transactionTypeId);

            if (transactionType is null)
            {
                return NotFound();
            }

            return Ok(transactionType);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionTypeDto>> CreateAsync(CreateTransactionTypeDto createTransactionTypeDto)
        {
            if (createTransactionTypeDto is null)
            {
                return BadRequest("Request body is null.");
            }

            var transactionType = await _transactionTypeService.CreateTransactionTypeAsync(createTransactionTypeDto);

            return Ok(transactionType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateTransactionTypeDto updateTransactionTypeDto)
        {
            if (updateTransactionTypeDto is null)
            {
                return BadRequest("Request body is null.");
            }

            await _transactionTypeService.UpdateTransactionTypeAsync(updateTransactionTypeDto);

            return NoContent();
        }

        [HttpDelete("{transactionTypeId}")]
        public async Task<IActionResult> DeleteAsync(Guid transactionTypeId)
        {
            if (transactionTypeId == Guid.Empty)
            {
                return BadRequest("Invalid transaction type ID.");
            }

            await _transactionTypeService.DeleteTransactionTypeAsync(transactionTypeId);

            return NoContent();
        }
    }
} 
