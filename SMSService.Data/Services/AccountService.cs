using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SMSService.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly SMSDataContext _context;

        public AccountService(SMSDataContext smsDataContext)
        {
            _context = smsDataContext;

        }


        public async Task AddAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var entity = await _context.Accounts.FirstOrDefaultAsync(t => t.Id == id);
            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccount(int id)
        {
            var account= await _context.Accounts.Include(a=> a.PhoneNumbers).FirstOrDefaultAsync(t => t.Id == id);
            return account;
        }

        public async Task<Account> GetUser(string username, string authId)
        {
            var user= await _context.Accounts.Include(a => a.PhoneNumbers).FirstOrDefaultAsync(a => a.Username == username && a.AuthId == authId);
            return user;
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _context.Accounts.Include(a => a.PhoneNumbers).ToListAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
