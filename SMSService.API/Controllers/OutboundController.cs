
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SMSSerivce.API.Authorization;
using SMSSerivce.API.Dtos;
using SMSSerivce.API.Models;
using SMSService.API.Services;

namespace SMSSerivce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OutboundController : ControllerBase
    {
        private readonly IAccountService _accountRepo;
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<AppSetting> _appSettings;

        public OutboundController(IAccountService accountRepo, IMemoryCache memoryCache, IOptions<AppSetting> appSettings)
        {
            _accountRepo = accountRepo;
            _memoryCache = memoryCache;
            _appSettings = appSettings;
        }

        [HttpPost]
        [Route("sms")]
        public async Task<IActionResult> SendSMS([FromBody] SendSMSDto sendSMSDto)
        {
            var response = new ResponseDto();
            try
            {

                var user = (Account)HttpContext.Items["User"];

                if (!user.PhoneNumbers.Any(x => x.Number == sendSMSDto.from))
                {
                    response.Message = "";
                    response.Error = "from parameter not found";

                    return BadRequest(response);
                }



                var to = sendSMSDto.to;
                var from = "";

                if (_memoryCache.TryGetValue(to, out from))
                {

                    response.Message = "";
                    response.Error = $"sms from {from} to {to} blocked by STOP request";

                    return BadRequest(response);
                }

                var count = 0;
                from = sendSMSDto.from;
                if (!_memoryCache.TryGetValue(from, out count))
                {
                    count = 1;
                    var cacheExpirationOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(24),
                        Priority = CacheItemPriority.Normal

                    };
                    _memoryCache.Set(from, count, cacheExpirationOptions);
                }
                if(count > _appSettings.Value.SMSLimit)
                {
                    response.Message = "";
                    response.Error = $"limit reached for from {from}";

                    return BadRequest(response);
                }

                _memoryCache.Set(from, count + 1);
                response.Message = "outbound sms ok";
                response.Error = "";

            }
            catch (Exception)
            {
                response.Message = "";
                response.Error = "unknown failure";

                return BadRequest(response);
            }




            return Ok(response);
        }



    }
}
