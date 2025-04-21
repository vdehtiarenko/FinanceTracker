using FinanceTracker.Application.DTO;
using FinanceTracker.Interfaces;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHttpService _httpService;

        public TransactionService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ApiResult<List<TransactionDto>>> GetTransactionsAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _httpService.GetAsync<List<TransactionDto>>(
                $"/api/Transaction/list?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");

            if (!response.IsSuccess)
                return ApiResult<List<TransactionDto>>.Failure($"Error fetching transactions: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<TransactionDto?>> GetTransactionByIdAsync(Guid transactionId)
        {
            var response = await _httpService.GetAsync<TransactionDto>($"/api/Transaction/{transactionId}");

            if (!response.IsSuccess)
                return ApiResult<TransactionDto?>.Failure($"Error fetching transaction: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<TransactionDto>> CreateTransactionAsync(CreateTransactionDto createTransactionDto)
        {
            var response = await _httpService.PostAsync<TransactionDto>("/api/Transaction", createTransactionDto);

            if (!response.IsSuccess)
                return ApiResult<TransactionDto>.Failure($"Failed to create transaction: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<TransactionDto>> UpdateTransactionAsync(UpdateTransactionDto updateTransactionDto)
        {
            var response = await _httpService.PutAsync<TransactionDto>($"/api/Transaction/{updateTransactionDto.Id}", updateTransactionDto);

            if (!response.IsSuccess)
                return ApiResult<TransactionDto>.Failure($"Failed to update transaction: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<bool>> DeleteTransactionAsync(Guid transactionId)
        {
            var response = await _httpService.DeleteAsync($"/api/Transaction/{transactionId}");

            if (!response.IsSuccess)
                return ApiResult<bool>.Failure($"Failed to delete transaction: {response.ErrorMessage}");

            return ApiResult<bool>.Success(true);
        }
    }
}
