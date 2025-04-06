using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iPortal.Data.Entities
{
    [Table("employers")]
    public class Employer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int userId { get; set; }
        public User user { get; set; }

        [Column("company_name")]
        public string companyName { get; set; }

        [Column("address")]
        public string address { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("phone_number")]
        public string phoneNumber { get; set; }

        [Column("website")]
        public string website { get; set; }
    }
}