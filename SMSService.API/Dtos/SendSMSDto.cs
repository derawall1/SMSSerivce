using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SMSSerivce.API.Dtos
{
    public class SendSMSDto
    {
        [Required(ErrorMessage = "from is missing")]
        [StringLength(16, ErrorMessage = "from is invalid", MinimumLength =6)]
        public string from { get; set; }

        [Required(ErrorMessage = "to is missing")]
        [StringLength(16, ErrorMessage = "to is invalid", MinimumLength = 6)]
        public string to { get; set; }

        [Required(ErrorMessage = "text is missing")]
        [StringLength(15, ErrorMessage = "text is invalid", MinimumLength = 1)]
        public string text { get; set; }
    }
}
