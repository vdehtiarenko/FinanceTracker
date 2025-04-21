namespace FinanceTracker.Application.Results
{
    namespace FinanceTracker.Application.Results
    {
        public class ApiResult<T>
        {
            public bool IsSuccess { get; set; }

            public T? Data { get; set; }

            public string? ErrorMessage { get; set; }

            public static ApiResult<T> Success(T? data) => new() { IsSuccess = true, Data = data };

            public static ApiResult<T> Failure(string error) => new() { IsSuccess = false, ErrorMessage = error };
        }
    }

}
