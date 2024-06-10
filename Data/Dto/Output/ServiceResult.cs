namespace Billing.Data.Dto.Output
{
    public class ServiceResult
    {
        public object? Data { get; }

        public ServiceResultError? Error { get; }

        public bool IsSuccess { get; }

        public ServiceResult(object? data, ServiceResultError? error)
        {
            Data = data;
            Error = error;

            if (Data != null && Error == null)
            {
                IsSuccess = true;
            }
        }
    }
}
