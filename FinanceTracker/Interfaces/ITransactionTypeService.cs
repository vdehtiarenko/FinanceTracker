using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Interfaces
{
    public interface ITransactionTypeService
    {
        Task<ApiResult<List<TransactionTypeDto>>> GetTransactionTypesAsync();

        Task<ApiResult<TransactionTypeDto?>> GetTransactionTypeByIdAsync(Guid transactionTypeId);

        Task<ApiResult<TransactionTypeDto>> CreateTransactionTypeAsync(CreateTransactionTypeDto createTransactionTypeDto);

        Task<ApiResult<TransactionTypeDto>> UpdateTransactionTypeAsync(UpdateTransactionTypeDto updateTransactionTypeDto);

        Task<ApiResult<bool>> DeleteTransactionTypeAsync(Guid transactionTypeId);
    }
}
