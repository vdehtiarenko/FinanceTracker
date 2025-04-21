using FinanceTracker.Application.DTO;

namespace FinanceTracker.Application.Interfaces
{
    public interface ITransactionTypeService
    {
        public Task<List<TransactionTypeDto>> GetTransactionTypesAsync();

        public Task<TransactionTypeDto> GetTransactionTypeByIdAsync(Guid transactionTypeId);

        public Task<TransactionTypeDto> CreateTransactionTypeAsync(CreateTransactionTypeDto createDto);

        public Task<TransactionTypeDto> UpdateTransactionTypeAsync(UpdateTransactionTypeDto updateDto);

        public Task DeleteTransactionTypeAsync(Guid transactionTypeId);
    }
}
