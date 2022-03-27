using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.Data
{
    [Table("account")]
    public class Account
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("auth_id")]
        public string AuthId { get; set; } = string.Empty;

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        


    }
}
