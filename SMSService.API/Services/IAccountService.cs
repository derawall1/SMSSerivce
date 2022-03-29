using SMSSerivce.API.Models;

namespace SMSService.API.Services
{
    public interface IAccountService
    {
        Task AddAccount(Account account);
        Task UpdateAccount(Account account);
        Task DeleteAccount(int id);
        Task<Account> GetAccount(int id);

        Task<Account> GetUser(string username, string authId);
        Task<List<Account>> GetAccounts();
    }
}
