namespace FinanceTracker.Application.DTO
{
    public class UpdateTransactionDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid TransactionTypeId { get; set; }
    }
}
