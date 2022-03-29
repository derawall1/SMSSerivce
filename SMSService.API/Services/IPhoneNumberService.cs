using SMSSerivce.API.Models;

namespace SMSService.API.Services
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
