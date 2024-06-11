namespace Services.Interfaces
{
    public interface IUserCheckingService
    {
        /// <summary>
        /// Check if the user is valid
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns>True if the user is valid, false - otherwise.</returns>
        bool IsUserValid(int userId);
    }
}