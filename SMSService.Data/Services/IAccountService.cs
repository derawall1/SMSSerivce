using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.Data.Services
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
