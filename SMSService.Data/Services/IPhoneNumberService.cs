using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.Data.Services
{
    public interface IPhoneNumberService
    {
        Task AddPhoneNumber(PhoneNumber phoneNumber);
        Task UpdatePhoneNumber(PhoneNumber phoneNumber);
        Task DeletePhoneNumber(int id);
        Task<PhoneNumber> GetPhoneNumber(int id);
        Task<bool> CheckPhoneNumber(string phoneNumber);
        Task<List<PhoneNumber>> GetPhoneNumbers();
    }
}
