using FinanceTracker.Application.Results.FinanceTracker.Application.Results;

namespace FinanceTracker.Interfaces
{
    public interface IHttpService
    {
        Task<ApiResult<T?>> GetAsync<T>(string uri);

        Task<ApiResult<T?>> PostAsync<T>(string uri, object? value);

        Task<ApiResult<T?>> PutAsync<T>(string uri, object? value);

        Task<ApiResult<bool>> DeleteAsync(string uri);
    }

}
