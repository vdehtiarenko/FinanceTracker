using FinanceTracker.Application.Results.FinanceTracker.Application.Results;
using FinanceTracker.Interfaces;

namespace FinanceTracker.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<T?>> GetAsync<T>(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    return ApiResult<T>.Failure($"GET failed: {response.StatusCode}");

                var data = await response.Content.ReadFromJsonAsync<T>();

                return ApiResult<T>.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure($"GET exception: {ex.Message}");
            }
        }

        public async Task<ApiResult<T?>> PostAsync<T>(string uri, object? value)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(uri, value);

                if (!response.IsSuccessStatusCode)
                    return ApiResult<T>.Failure($"POST failed: {response.StatusCode}");

                var data = await response.Content.ReadFromJsonAsync<T>();

                return ApiResult<T>.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure($"POST exception: {ex.Message}");
            }
        }

        public async Task<ApiResult<T?>> PutAsync<T>(string uri, object? value)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(uri, value);

                if (!response.IsSuccessStatusCode)
                    return ApiResult<T>.Failure($"PUT failed: {response.StatusCode}");

                var data = await response.Content.ReadFromJsonAsync<T>();

                return ApiResult<T>.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure($"PUT exception: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string uri)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode)
                    return ApiResult<bool>.Failure($"DELETE failed: {response.StatusCode}");

                return ApiResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failure($"DELETE exception: {ex.Message}");
            }
        }
    }

}
