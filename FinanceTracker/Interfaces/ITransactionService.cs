using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Interfaces
{
    public interface ITransactionService
    {
        Task<ApiResult<List<TransactionDto>>> GetTransactionsAsync(DateTime startDate, DateTime endDate);

        Task<ApiResult<TransactionDto?>> GetTransactionByIdAsync(Guid transactionId);

        Task<ApiResult<TransactionDto>> CreateTransactionAsync(CreateTransactionDto createTransactionDto);

        Task<ApiResult<TransactionDto>> UpdateTransactionAsync(UpdateTransactionDto updateTransactionDto);

        Task<ApiResult<bool>> DeleteTransactionAsync(Guid transactionId);
    }
}
