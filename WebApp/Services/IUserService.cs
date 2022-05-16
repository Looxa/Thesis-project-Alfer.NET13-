using System.Security.Claims;
using FileSharer.Web.Data.EntityF;

namespace FileSharer.Web.Services
{
    public interface IUserService
    {
        Task<AccountInfo?> GetAccountInfo(string login);
        Task<IEnumerable<Claim>> GetUserClaims(AccountInfo accountInfo);
    }
}
