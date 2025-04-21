using FinanceTracker.Application.DTO;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Interfaces
{
    public interface IReportService
    {
        Task<ApiResult<DailyReport?>> GetDailyReportAsync(DateTime date);

        Task<ApiResult<DatePeriodReport?>> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate);
    }
}
