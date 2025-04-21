namespace FinanceTracker.Application.DTO
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid TransactionTypeId { get; set; }
    }
}
