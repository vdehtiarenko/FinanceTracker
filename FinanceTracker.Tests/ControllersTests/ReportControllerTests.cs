using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.DTO;
using FinanceTracker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinanceTracker.Tests.ControllersTests
{
    public class ReportControllerTests
    {
        private readonly Mock<ITransactionService> _mockTransactionService;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
            _controller = new ReportController(_mockTransactionService.Object);
        }

        [Fact]
        public async Task GetDailyReportAsync_ReturnsBadRequest_WhenDateIsMinValue()
        {
            // Arrange

            var date = DateTime.MinValue;

            // Act

            var result = await _controller.GetDailyReportAsync(date);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDailyReportAsync_ReturnsBadRequest_WhenDateIsInFuture()
        {

            // Arrange
            var date = DateTime.UtcNow.AddDays(1);

            // Act

            var result = await _controller.GetDailyReportAsync(date);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Date cannot be in the future.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDatePeriodReporttAsync_ReturnsBadRequest_WhenStartDateIsMinValue()
        {
            // Arrange

            var startDate = DateTime.MinValue;
            var endDate = DateTime.UtcNow;

            // Act

            var result = await _controller.GetDatePeriodReporttAsync(startDate, endDate);


            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date values.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDatePeriodReporttAsync_ReturnsBadRequest_WhenEndDateIsMinValue()
        {
            // Arrange

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.MinValue;

            // Act

            var result = await _controller.GetDatePeriodReporttAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid date values.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDatePeriodReporttAsync_ReturnsBadRequest_WhenStartDateIsGreaterThanEndDate()
        {
            // Arrange

            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow.AddDays(-1);

            // Act

            var result = await _controller.GetDatePeriodReporttAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Start date cannot be later than end date.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDatePeriodReporttAsync_ReturnsBadRequest_WhenStartDateIsInFuture()
        {
            // Arrange

            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(2);

            // Act

            var result = await _controller.GetDatePeriodReporttAsync(startDate, endDate);

            // Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Start date cannot be in the future.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDatePeriodReporttAsync_ReturnsBadRequest_WhenEndDateIsInFuture()
        {
            // Arrange

            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow.AddDays(1);

            // Act

            var result = await _controller.GetDatePeriodReporttAsync(startDate, endDate);


            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("End date cannot be in the future.", badRequestResult.Value);
        }
    }
}
