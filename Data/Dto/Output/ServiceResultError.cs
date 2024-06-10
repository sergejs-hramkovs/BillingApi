using System.Text;

namespace Billing.Data.Dto.Output
{
    public class ServiceResultError
    {
        public string ErrorMessage { get; }

        public ServiceResultError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ServiceResultError(string errorMessage, string? exception, string? innerException)
        {
            StringBuilder sb = new();
            sb.AppendLine(errorMessage);
            sb.AppendLine(exception);
            sb.AppendLine(innerException);

            ErrorMessage = sb.ToString();
        }
    }
}
