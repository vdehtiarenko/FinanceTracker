using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TransactionType>()
                .Property(t => t.Category)
                .HasConversion<string>();

            var salaryTypeId = new Guid("f06c2b13-5d3c-4bfb-9c8f-81968b1c43b7");
            var groceriesTypeId = new Guid("b6a3c8f5-3a4f-4c66-8a4b-5d2f9f1c6a1a");
            var rentTypeId = new Guid("d2b1f9e2-8a0f-4a7b-9c5b-6e4e6f8d3c5b");
            var freelanceTypeId = new Guid("e3f9d6b2-7c5b-4a0f-8a9b-1a2f4e7b9d6c");
            var utilitiesTypeId = new Guid("d2b2b59b-f56f-49ec-88f4-3833176317b1");
            var transportTypeId = new Guid("3451d82d-b1c4-48d4-b8f4-9ab9c348db70");
            var entertainmentTypeId = new Guid("c3b9a36d-e9e1-457a-9153-dce7ff6b3876");

            var fixedSalaryTransactionDate = new DateTime(2025, 03, 10);
            var fixedFreelanceTransactionDate = new DateTime(2025, 03, 15);

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType(salaryTypeId, "Salary", TransactionCategory.Income, "Monthly salary payment"),
                new TransactionType(groceriesTypeId, "Groceries", TransactionCategory.Expense, "Purchases for household items"),
                new TransactionType(rentTypeId, "Rent", TransactionCategory.Expense, "Monthly rent payment"),
                new TransactionType(freelanceTypeId, "Freelance", TransactionCategory.Income, "Payment for freelance work"),
                new TransactionType(utilitiesTypeId, "Utilities", TransactionCategory.Expense, "Electricity, water, internet, etc."),
                new TransactionType(transportTypeId, "Transport", TransactionCategory.Expense, "Gas, car insurance, etc."),
                new TransactionType(entertainmentTypeId, "Entertainment", TransactionCategory.Expense, "Subscriptions, dining out, etc.")
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(new Guid("d550f92d-f5e7-4a72-8f8d-406db0b1cfa1"), 3000, fixedSalaryTransactionDate, salaryTypeId),
                new Transaction(new Guid("f4b98a8d-1ffb-4c02-b024-bba05b8d758b"), 500, fixedFreelanceTransactionDate, freelanceTypeId)
            );

            var currentDate = new DateTime(2025, 4, 11); 


            modelBuilder.Entity<Transaction>().HasData(
    new Transaction(new Guid("6eec8a6d-8b82-4f97-8689-ea60a1a1b3d4"), 100, currentDate.AddDays(-30), groceriesTypeId),
    new Transaction(new Guid("76dbf80f-d551-4dbb-a2b5-df56cba07996"), 120, currentDate.AddDays(-29), groceriesTypeId),
    new Transaction(new Guid("ee421174-59de-4625-b80f-5401494da1a3"), 80, currentDate.AddDays(-28), groceriesTypeId),
    new Transaction(new Guid("f03da89c-dc44-47b5-853e-9edc5181cd74"), 1600, currentDate.AddDays(-27), salaryTypeId),
    new Transaction(new Guid("fa0b3a9e-7b73-46f9-8a85-8f12a41a3ac7"), 1500, currentDate.AddDays(-26), salaryTypeId),
    new Transaction(new Guid("c69fbd5c-8ad9-49b7-97d0-0867a6d3ae92"), 60, currentDate.AddDays(-25), entertainmentTypeId),
    new Transaction(new Guid("1be27d66-cfe6-4bcf-9f2e-4f5e1584648e"), 70, currentDate.AddDays(-24), entertainmentTypeId),
    new Transaction(new Guid("d96097b2-4295-4180-b44c-6409d5687584"), 110, currentDate.AddDays(-23), utilitiesTypeId),
    new Transaction(new Guid("8d504fc7-9451-4e66-95c7-cbe69454bdbf"), 130, currentDate.AddDays(-22), utilitiesTypeId),
    new Transaction(new Guid("d75d510b-75b5-4d3f-b218-3e5ca84db342"), 140, currentDate.AddDays(-21), utilitiesTypeId),
    new Transaction(new Guid("71a0a764-baba-45b1-8d8d-9907f35b497d"), 110, currentDate.AddDays(-20), groceriesTypeId),
    new Transaction(new Guid("e1d29dbb-e09c-467e-bc11-69d071f8d77b"), 90, currentDate.AddDays(-19), groceriesTypeId),
    new Transaction(new Guid("207bce3e-d25d-4327-b717-19ef5f6b8102"), 120, currentDate.AddDays(-18), groceriesTypeId),
    new Transaction(new Guid("60381e39-bb99-4d0f-9778-9e3b35bc1b61"), 130, currentDate.AddDays(-17), groceriesTypeId),
    new Transaction(new Guid("c9fe7c9a-1a2d-4f42-9508-6347a7d32f5a"), 100, currentDate.AddDays(-16), groceriesTypeId),
    new Transaction(new Guid("3c07a5bb-4587-4d91-b3ea-c8fc578c5639"), 110, currentDate.AddDays(-15), groceriesTypeId),
    new Transaction(new Guid("fd1b86e5-570d-48fd-9889-d799935b0d4e"), 120, currentDate.AddDays(-14), groceriesTypeId),
    new Transaction(new Guid("b7ea937d-b763-466b-8725-bfcb42d2e312"), 90, currentDate.AddDays(-13), groceriesTypeId),
    new Transaction(new Guid("e4c8d0f5-8ed5-4525-bc42-8e3a9f9c5158"), 100, currentDate.AddDays(-12), groceriesTypeId),
    new Transaction(new Guid("c4a15a2d-41a0-4be2-b8a7-8eec8db2a70c"), 110, currentDate.AddDays(-11), groceriesTypeId),
    new Transaction(new Guid("c27f44b7-3d12-4bda-bb13-9e6f5d8bfb02"), 120, currentDate.AddDays(-10), groceriesTypeId),
    new Transaction(new Guid("b75e003b-e8b2-48f7-b2ec-9fd510517607"), 80, currentDate.AddDays(-9), groceriesTypeId),
    new Transaction(new Guid("87d0e3c6-f7d6-45f4-b63b-df204c402030"), 90, currentDate.AddDays(-8), groceriesTypeId),
    new Transaction(new Guid("ad81393e-d98f-41ae-830f-fdc07a040c15"), 100, currentDate.AddDays(-7), groceriesTypeId),
    new Transaction(new Guid("847167d9-15b3-413d-83f2-69475c742b06"), 110, currentDate.AddDays(-6), groceriesTypeId),
    new Transaction(new Guid("2644b8f3-931b-413e-8d61-29f35fe3d5ac"), 120, currentDate.AddDays(-5), groceriesTypeId),
    new Transaction(new Guid("446c25b9-ff97-471e-a72a-e6f967b7d8be"), 130, currentDate.AddDays(-4), groceriesTypeId),
    new Transaction(new Guid("71df6c76-d5a0-4e44-b01c-00918b31a7c3"), 140, currentDate.AddDays(-3), groceriesTypeId),
    new Transaction(new Guid("a85b1155-1227-46a3-8919-22b8b349e7ab"), 150, currentDate.AddDays(-2), groceriesTypeId),
    new Transaction(new Guid("3b6cf6c7-49d3-4e9e-bf7d-408611aeb91f"), 160, currentDate.AddDays(-1), groceriesTypeId),
    new Transaction(new Guid("4c9b0b5b-c0f7-4be0-8c7b-17d6fdc5bb8e"), 1600, currentDate.AddDays(-1), salaryTypeId),
    new Transaction(new Guid("5c6a5747-6f4f-42f2-bab0-3e451ee3a574"), 1500, currentDate.AddDays(-2), salaryTypeId),
    new Transaction(new Guid("7da542cb-50da-46cc-b6c8-61690f62dba7"), 60, currentDate.AddDays(-3), entertainmentTypeId),
    new Transaction(new Guid("4b1a9a6d-6d9e-475b-8b80-ff1574209b09"), 50, currentDate.AddDays(-4), entertainmentTypeId),
    new Transaction(new Guid("2fd60a9b-70d3-47ed-bde1-8265b67d9b3f"), 130, currentDate.AddDays(-5), utilitiesTypeId),
    new Transaction(new Guid("09df1b34-e212-4c92-ae63-dc70b949a890"), 140, currentDate.AddDays(-6), utilitiesTypeId),
    new Transaction(new Guid("e8c2f44e-7b13-4e4d-b109-c97a1b9e1e73"), 150, currentDate.AddDays(-7), utilitiesTypeId)
);
        }


        public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>()) { }
    }
}
