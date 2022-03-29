using Microsoft.EntityFrameworkCore;
using SMSSerivce.API.Models;
using SMSService.API.Data;

namespace SMSService.API.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly SMSDataContext _context;

        public PhoneNumberService(SMSDataContext smsDataContext)
        {
            _context = smsDataContext;

        }
        public async Task AddPhoneNumber(PhoneNumber phoneNumber)
        {
            await _context.PhoneNumbers.AddAsync(phoneNumber);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckPhoneNumber(string phoneNumber)
        {
            var result = await _context.PhoneNumbers.AnyAsync(x=>x.Number == phoneNumber);
            return result;
        }

        public async Task DeletePhoneNumber(int id)
        {
            var entity = await _context.PhoneNumbers.FirstOrDefaultAsync(t => t.Id == id);
            if (entity != null)
            {
                _context.PhoneNumbers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PhoneNumber> GetPhoneNumber(int id)
        {
            return  await _context.PhoneNumbers.Include(p=>p.Account).FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task<List<PhoneNumber>> GetPhoneNumbers()
        {
            return await _context.PhoneNumbers.Include(a => a.Account).ToListAsync();
        }

        public async Task UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Update(phoneNumber);
            await _context.SaveChangesAsync();
        }
    }
}
