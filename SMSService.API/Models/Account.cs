using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSSerivce.API.Models
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
