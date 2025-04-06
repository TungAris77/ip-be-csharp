using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iPortal.Data.Entities
{
    [Table("token")]
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Column("username")]
        public string username { get; set; }

        [Column("token")]
        public string token { get; set; }

        [Column("expiration")]
        public DateTime expiration { get; set; }

        public Token() { }

        public Token(string username, string token, DateTime expiration)
        {
            this.username = username;
            this.token = token;
            this.expiration = expiration;
        }
    }
}