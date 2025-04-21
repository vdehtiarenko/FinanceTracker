using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.DTO
{
    public class TransactionTypeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TransactionCategory Category { get; set; }

        public string Description { get; set; }
    }
}
