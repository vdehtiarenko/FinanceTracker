using Moq;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.DAL;
using FinanceTracker.Infrastructure.Services;
using FinanceTracker.Application.DTO;

namespace FinanceTracker.Tests.ServicesTests
{
    public class TransactionServiceTests
    {
        private readonly Mock<DbSet<Transaction>> _mockTransactionDbSet;
        private readonly Mock<DbSet<TransactionType>> _mockTransactionTypeDbSet;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(Guid.NewGuid(), 1000, DateTime.Now, Guid.NewGuid()),
                new Transaction(Guid.NewGuid(), 200, DateTime.Now, Guid.NewGuid())
            };

            var transactionTypes = new List<TransactionType>
            {
                new TransactionType(Guid.NewGuid(), "Salary", TransactionCategory.Income, "Monthly salary"),
                new TransactionType(Guid.NewGuid(), "Groceries", TransactionCategory.Expense, "Food and supplies")
            };

            _mockTransactionDbSet = CreateMockDbSet(transactions);
            _mockTransactionTypeDbSet = CreateMockDbSet(transactionTypes);

            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockDbContext.Setup(db => db.Transactions).Returns(_mockTransactionDbSet.Object);
            _mockDbContext.Setup(db => db.TransactionTypes).Returns(_mockTransactionTypeDbSet.Object);

            _transactionService = new TransactionService(_mockDbContext.Object);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_CorrectlyThrowsArgumentExceptionWhenTransactionIdIsEmpty()
        {
            // Arrange

            Guid transactionId = Guid.Empty;

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionService.GetTransactionByIdAsync(transactionId));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction ID cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_CorrectlyThrowsArgumentExceptionWhenTransactionTypeIdIsEmpty()
        {
            // Arrange

            var createTransactionDto = new CreateTransactionDto
            {
                Amount = 1000,
                Date = DateTime.Now,
                TransactionTypeId = Guid.Empty  
            };

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionService.CreateTransactioneAsync(createTransactionDto));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type ID cannot be empty.", exception.Message);
        }


        [Fact]
        public async Task UpdateTransactionAsync_CorrectlyThrowsArgumentExceptionWhenTransactionIdIsEmpty()
        {
            // Arrange

            var updateTransactionDto = new UpdateTransactionDto
            {
                Id = Guid.Empty,  
                Amount = 1000,               
                Date = DateTime.Now,         
                TransactionTypeId = Guid.NewGuid()  
            };

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionService.UpdateTransactionAsync(updateTransactionDto));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction ID cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteTransactionAsync_CorrectlyThrowsArgumentExceptionWhenTransactionIdIsEmpty()
        {
            // Arrange

            Guid transactionId = Guid.Empty;

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionService.DeleteTransactionAsync(transactionId));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction ID cannot be empty.", exception.Message);
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(IList<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockSet;
        }
    }
}
