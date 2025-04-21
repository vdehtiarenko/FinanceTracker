using FinanceTracker.Application.DTO;
using FinanceTracker.Interfaces;
using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IHttpService _httpService;

        public TransactionTypeService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ApiResult<List<TransactionTypeDto>>> GetTransactionTypesAsync()
        {
            var response = await _httpService.GetAsync<List<TransactionTypeDto>>("/api/TransactionType");

            if (!response.IsSuccess)
                return ApiResult<List<TransactionTypeDto>>.Failure(response.ErrorMessage);

            return response;
        }

        public async Task<ApiResult<TransactionTypeDto?>> GetTransactionTypeByIdAsync(Guid transactionTypeId)
        {
            var response = await _httpService.GetAsync<TransactionTypeDto>($"/api/TransactionType/{transactionTypeId}");

            if (!response.IsSuccess)
                return ApiResult<TransactionTypeDto?>.Failure(response.ErrorMessage);

            return response;
        }

        public async Task<ApiResult<TransactionTypeDto>> CreateTransactionTypeAsync(CreateTransactionTypeDto createTransactionTypeDto)
        {
            var response = await _httpService.PostAsync<TransactionTypeDto>("/api/TransactionType", createTransactionTypeDto);

            if (!response.IsSuccess)
                return ApiResult<TransactionTypeDto>.Failure($"Failed to create transaction type: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<TransactionTypeDto>> UpdateTransactionTypeAsync(UpdateTransactionTypeDto updateTransactionTypeDto)
        {
            var response = await _httpService.PutAsync<TransactionTypeDto>($"/api/TransactionType/{updateTransactionTypeDto.Id}", updateTransactionTypeDto);

            if (!response.IsSuccess)
                return ApiResult<TransactionTypeDto>.Failure($"Failed to update transaction type: {response.ErrorMessage}");

            return response;
        }

        public async Task<ApiResult<bool>> DeleteTransactionTypeAsync(Guid transactionTypeId)
        {
            var response = await _httpService.DeleteAsync($"/api/TransactionType/{transactionTypeId}");

            if (!response.IsSuccess)
                return ApiResult<bool>.Failure($"Failed to delete transaction type: {response.ErrorMessage}");

            return ApiResult<bool>.Success(true);
        }
    }
}
