using Moq;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Application.DTO;

namespace FinanceTracker.Tests.ControllersTests
{
    public class TransactionTypeControllerTests
    {
        private readonly Mock<ITransactionTypeService> _mockTransactionTypeService;
        private readonly TransactionTypeController _controller;

        public TransactionTypeControllerTests()
        {
            _mockTransactionTypeService = new Mock<ITransactionTypeService>();
            _controller = new TransactionTypeController(_mockTransactionTypeService.Object);
        }

        [Fact]
        public async Task GetTransactionTypeByIdAsync_ReturnsBadRequest_WhenTransactionTypeIdIsEmpty()
        {
            // Arrange

            var transactionTypeId = Guid.Empty;

            // Act

            var result = await _controller.GetByIdAsync(transactionTypeId);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid transaction type ID.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateTransactionTypeAsync_ReturnsBadRequest_WhenCreateTransactionTypeDtoIsNull()
        {
            // Arrange

            CreateTransactionTypeDto createTransactionTypeDto = null;

            // Act

            var result = await _controller.CreateAsync(createTransactionTypeDto);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Request body is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateTransactionTypeAsync_ReturnsBadRequest_WhenUpdateTransactionTypeDtoIsNull()
        {
            // Arrange

            var id = Guid.Empty;
            UpdateTransactionTypeDto updateTransactionTypeDto = null;

            // Act

            var result = await _controller.UpdateAsync(id, updateTransactionTypeDto);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Request body is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteTransactionTypeAsync_ReturnsBadRequest_WhenTransactionTypeIdIsEmpty()
        {
            // Arrange

            var transactionTypeId = Guid.Empty;

            // Act

            var result = await _controller.DeleteAsync(transactionTypeId);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid transaction type ID.", badRequestResult.Value);
        }
    }
}
