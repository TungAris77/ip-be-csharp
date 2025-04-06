using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iPortal.Data.Entities
{
    [Table("token")]
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string TokenValue { get; set; }
        public DateTime Expiration { get; set; }

        public Token() { }

        public Token(string username, string token, DateTime expiration)
        {
            Username = username;
            TokenValue = token;
            Expiration = expiration;
        }
    }
}