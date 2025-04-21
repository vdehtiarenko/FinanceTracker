using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.DTO
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
