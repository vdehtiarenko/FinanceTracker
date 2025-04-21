using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly ITransactionService _transactionService;

        public ReportController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("date")]
        public async Task<ActionResult<DailyReport>> GetDailyReportAsync([FromQuery] DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return BadRequest("Invalid date.");
            }

            if (date.Date > DateTime.Now.Date)
            {
                return BadRequest("Date cannot be in the future.");
            }

            var dailyReport = await _transactionService.GetDailyReportAsync(date);

            if (!dailyReport.Transactions.Any())
            {
                dailyReport = new DailyReport(date, 0, 0, new List<Transaction>());
            }        

            return Ok(dailyReport);
        }

        [HttpGet("period")]
        public async Task<ActionResult<DatePeriodReport>> GetDatePeriodReporttAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                return BadRequest("Invalid date values.");
            }

            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            if (startDate.Date > DateTime.Now.Date)
            {
                return BadRequest("Start date cannot be in the future.");
            }

            if (endDate.Date > DateTime.Now.Date)
            {
                return BadRequest("End date cannot be in the future.");
            }

            var periodReport = await _transactionService.GetDatePeriodReportAsync(startDate, endDate);

            if (!periodReport.Transactions.Any())
            {
                return Ok(new DatePeriodReport
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalIncome = 0,
                    TotalExpenses = 0,
                    Transactions = new List<Transaction>()
                });
            }

            return Ok(periodReport);
        }
    }
}
