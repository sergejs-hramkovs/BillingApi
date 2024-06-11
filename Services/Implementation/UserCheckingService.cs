using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class UserCheckingService : IUserCheckingService
    {
        private readonly ILogger<UserCheckingService> _logger;

        public UserCheckingService(ILogger<UserCheckingService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Check if the user is valid
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns>True if the user is valid, false - otherwise.</returns>
        public bool IsUserValid(int userId)
        {
            _logger.LogInformation(
                "[{Class} - {Method}]: Checking user, UserId: {Id}",
                nameof(UserCheckingService),
                nameof(IsUserValid),
                userId);

            Random random = new();

            var number = random.Next(1, 10);

            if (userId / number % 2 == 0)
            {
                _logger.LogInformation(
                    "[{Class} - {Method}]: The user is valid, UserId: {Id}",
                    nameof(UserCheckingService),
                    nameof(IsUserValid),
                    userId);

                return true;
            }

            _logger.LogError(
                "[{Class} - {Method}]: The user is not valid, UserId: {Id}",
                nameof(UserCheckingService),
                nameof(IsUserValid),
                userId);

            return false;
        }
    }
}
