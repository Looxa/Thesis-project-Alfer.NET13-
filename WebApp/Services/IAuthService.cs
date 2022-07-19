namespace FileSharer.Web.Services
{
    public interface IAuthService
    {
        Task<bool> Login(string login, string password);
        Task Logout();
        bool IsAuthenticated { get; }
    }
}
