using FinanceTracker.Application.DTO;

namespace FinanceTracker.Application.Interfaces
{
    public interface ITransactionService
    {
        public Task<List<TransactionDto>> GetTransactionsAsync(DateTime startDate, DateTime endDate);

        public Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId);

        public Task<TransactionDto> CreateTransactioneAsync(CreateTransactionDto createTransactionDto);

        public Task<TransactionDto> UpdateTransactionAsync(UpdateTransactionDto updateTransactionDto);

        public Task DeleteTransactionAsync(Guid transactionId);

        public Task<DailyReport> GetDailyReportAsync(DateTime date);

        public Task<DatePeriodReport> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate);
    }
}
