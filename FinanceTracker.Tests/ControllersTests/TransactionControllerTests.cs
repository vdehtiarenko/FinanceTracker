using Moq;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Application.DTO;

namespace FinanceTracker.Tests.ControllersTests
{
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> _mockTransactionService;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
            _controller = new TransactionController(_mockTransactionService.Object);
        }

        [Fact]
        public async Task GetTransactionByIdAsync_ReturnsBadRequest_WhenTransactionIdIsEmpty()
        {
            // Arrange

            var transactionId = Guid.Empty;

            // Act

            var result = await _controller.GetByIdAsync(transactionId);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid transaction ID.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateTransactionAsync_ReturnsBadRequest_WhenCreateTransactionDtoIsNull()
        {
            // Arrange

            CreateTransactionDto createTransactionDto = null;

            // Act

            var result = await _controller.CreateAsync(createTransactionDto);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);  
            Assert.Equal("Request body is null.", badRequestResult.Value);  
        }



        [Fact]
        public async Task UpdateTransactionAsync_ReturnsBadRequest_WhenUpdateTransactionDtoIsNull()
        {
            // Arrange

            var id = Guid.Empty;
            UpdateTransactionDto updateTransactionDto = null;

            // Act

            var result = await _controller.UpdateAsync(id, updateTransactionDto);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Request body is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteTransactionAsync_ReturnsBadRequest_WhenTransactionIdIsEmpty()
        {
            // Arrange

            var transactionId = Guid.Empty;

            // Act

            var result = await _controller.DeleteAsync(transactionId);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid transaction ID.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetListAsync_ReturnsBadRequest_WhenStartDateIsMinValue()
        {
            // Arrange

            var startDate = DateTime.MinValue;
            var endDate = DateTime.UtcNow;

            // Act

            var result = await _controller.GetListAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date values.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetListAsync_ReturnsBadRequest_WhenEndDateIsMinValue()
        {
            // Arrange

            var startDate = DateTime.UtcNow;
            var endDate = DateTime.MinValue;

            // Act

            var result = await _controller.GetListAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date values.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetListAsync_ReturnsBadRequest_WhenStartDateIsGreaterThanEndDate()
        {
            // Arrange

            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow.AddDays(-1);

            // Act

            var result = await _controller.GetListAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Start date cannot be later than end date.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetListAsync_ReturnsBadRequest_WhenStartDateIsInFuture()
        {
            // Arrange

            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);

            // Act
            var result = await _controller.GetListAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Start date cannot be in the future.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetListAsync_ReturnsBadRequest_WhenEndDateIsInFuture()
        {
            // Arrange

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(1);

            // Act

            var result = await _controller.GetListAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("End date cannot be in the future.", badRequestResult.Value);
        }
    }
}
