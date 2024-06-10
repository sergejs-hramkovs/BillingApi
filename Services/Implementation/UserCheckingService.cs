using Services.Interfaces;

namespace Services.Implementation
{
    public class UserCheckingService : IUserCheckingService
    {
        public UserCheckingService()
        {

        }

        public bool IsUserValid(int userId)
        {
            Random random = new();

            var number = random.Next(0, 10);

            if (userId / number % 2 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
