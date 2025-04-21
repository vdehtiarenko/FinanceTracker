using FinanceTracker.Application.DTO;
using FinanceTracker.Interfaces;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Services
{
    public class ReportService : IReportService
    {
        private readonly IHttpService _httpService;

        public ReportService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ApiResult<DailyReport?>> GetDailyReportAsync(DateTime date)
        {
            var response = await _httpService.GetAsync<DailyReport>($"/api/Report/date?date={date:yyyy-MM-dd}");

            if (!response.IsSuccess)
                return ApiResult<DailyReport?>.Failure(response.ErrorMessage);

            return response;
        }

        public async Task<ApiResult<DatePeriodReport?>> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _httpService.GetAsync<DatePeriodReport>($"/api/Report/period?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");

            if (!response.IsSuccess)
                return ApiResult<DatePeriodReport?>.Failure(response.ErrorMessage);

            return response;
        }
    }
}
