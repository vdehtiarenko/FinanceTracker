using Moq;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.DAL;
using FinanceTracker.Infrastructure.Services;
using FinanceTracker.Application.DTO;

namespace FinanceTracker.Tests.ServicesTests
{
    public class TransactionTypeServiceTests
    {
        private readonly Mock<DbSet<TransactionType>> _mockTransactionTypeDbSet;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly TransactionTypeService _transactionTypeService;

        public TransactionTypeServiceTests()
        {
            var transactionTypes = new List<TransactionType>
            {
                new TransactionType(Guid.NewGuid(), "Salary", TransactionCategory.Income, "Monthly salary"),
                new TransactionType(Guid.NewGuid(), "Groceries", TransactionCategory.Expense, "Food and supplies")
            };

            _mockTransactionTypeDbSet = CreateMockDbSet(transactionTypes);

            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockDbContext.Setup(db => db.TransactionTypes).Returns(_mockTransactionTypeDbSet.Object);

            _transactionTypeService = new TransactionTypeService(_mockDbContext.Object);
        }

        [Fact]
        public async Task GetTransactionTypeByIdAsync_CorrectlyThrowsArgumentExceptionWhenTransactionTypeIdIsEmpty()
        {
            // Arrange

            Guid transactionTypeId = Guid.Empty;

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.GetTransactionTypeByIdAsync(transactionTypeId));

            //Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type ID cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task CreateTransactionTypeAsync_CorrectlyThrowsArgumentExceptionWhenNameIsEmpty()
        {
            // Arrange

            var createTransactionTypeDto = new CreateTransactionTypeDto
            {
                Name = string.Empty,  
                Category = TransactionCategory.Expense,
                Description = "Food and supplies"
            };

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.CreateTransactionTypeAsync(createTransactionTypeDto));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type name cannot be empty.", exception.Message);
        }


        [Fact]
        public async Task CreateTransactionTypeAsync_CorrectlyThrowsArgumentExceptionWhenDescriptionIsEmpty()
        {
            // Arrange

            var createTransactionTypeDto = new CreateTransactionTypeDto
            {
                Name = "Salary",
                Category = TransactionCategory.Expense,
                Description = string.Empty
            }; 

            // Act

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.CreateTransactionTypeAsync(createTransactionTypeDto));

            //Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type description cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task UpdateTransactionTypeAsync_CorrectlyThrowsArgumentExceptionWhenNameIsEmpty()
        {
            // Arrange

            var updateTransactionTypeDto = new UpdateTransactionTypeDto
            {
                Id = Guid.NewGuid(),  
                Name = string.Empty,   
                Category = TransactionCategory.Expense,
                Description = "Updated monthly salary description"
            };

            // Act 

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.UpdateTransactionTypeAsync(updateTransactionTypeDto));

            // Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type name cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task UpdateTransactionTypeAsync_CorrectlyThrowsArgumentExceptionWhenDescriptionIsEmpty()
        {
            // Arrange

            var updateTransactionTypeDto = new UpdateTransactionTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Salary",
                Category = TransactionCategory.Expense,
                Description = null
            };

            // Act 

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.UpdateTransactionTypeAsync(updateTransactionTypeDto));

            //Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type description cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteTransactionTypeAsync_CorrectlyThrowsArgumentExceptionWhenTransactionTypeIdIsEmpty()
        {
            // Arrange

            Guid transactionTypeId = Guid.Empty;

            // Act 

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _transactionTypeService.DeleteTransactionTypeAsync(transactionTypeId));

            //Assert

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("The transaction type ID cannot be empty.", exception.Message);
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
