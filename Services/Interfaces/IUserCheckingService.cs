namespace Services.Interfaces
{
    public interface IUserCheckingService
    {
        bool IsUserValid(int userId);
    }
}