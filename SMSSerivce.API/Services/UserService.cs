

using SMSService.Data;
using SMSService.Data.Services;

namespace SMSService.API.Services
{

    public interface IUserService
    {
        Task<Account> Authenticate(string username, string authId);
       
    }

    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;

        public UserService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> Authenticate(string username, string authId)
        {
            
            var user = await _accountService.GetUser(username, authId);

           
            return user;
        }

    }
}