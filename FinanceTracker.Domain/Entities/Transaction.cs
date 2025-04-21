namespace FinanceTracker.Domain.Entities 
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

        public Transaction(Guid id, decimal amount, DateTime date, Guid transactionTypeId)
        {
            Id = id;
            Amount = amount;
            Date = date;
            TransactionTypeId = transactionTypeId;
        }
    }
}
