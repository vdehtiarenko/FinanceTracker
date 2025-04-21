namespace FinanceTracker.Domain.Entities
{
    public class TransactionType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TransactionCategory Category { get; set; }

        public string Description { get; set; }

        public TransactionType() { }
      
        public TransactionType(Guid id, string name, TransactionCategory category, string description)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
        }
    }
}
