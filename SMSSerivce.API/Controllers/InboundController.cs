
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SMSSerivce.API.Authorization;
using SMSSerivce.API.Dtos;
using SMSService.Data;
using SMSService.Data.Services;
using System.Net;
using System.Text.RegularExpressions;

namespace SMSSerivce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InboundController : ControllerBase
    {
        private readonly IAccountService _accountRepo;       
        private readonly IMemoryCache _memoryCache;

        public InboundController(IAccountService accountRepo, IMemoryCache memoryCache)
        {
            _accountRepo = accountRepo;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("sms")]
        public async Task<IActionResult> SendSMS([FromBody] SendSMSDto sendSMSDto)
        {
            var response = new ResponseDto();
            try
            {                
                
                var user = (Account)HttpContext.Items["User"];
                 
                if(!user.PhoneNumbers.Any(x=>x.Number == sendSMSDto.to))
                {
                    response.Message = "";
                    response.Error = "to parameter not found";

                    return BadRequest(response);
                }

               
                var searchString = "STOP";
               
                var sentence = sendSMSDto.text;
              
                var to ="";
                var from = sendSMSDto.from;
                if (sentence.Contains(searchString))         
                {
                   if(!_memoryCache.TryGetValue(from, out to))
                    {
                        var cacheExpirationOptions = new MemoryCacheEntryOptions()
                        {
                            AbsoluteExpiration = DateTime.Now.AddHours(4),
                            Priority = CacheItemPriority.Normal

                        };
                        _memoryCache.Set(from,sendSMSDto.to,cacheExpirationOptions);
                    }

                }

                response.Message = "inbound sms ok";
                response.Error = "";

            }
            catch (Exception)
            {
                response.Message = "";
                response.Error = "unknown failure";

                return BadRequest(response) ;
            }
            


           
            return Ok(response);
        }

       
        
    }
}
