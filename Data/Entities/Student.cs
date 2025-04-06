using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iPortal.Data.Entities
{
    [Table("students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int userId { get; set; }
        public User user { get; set; }

        [Column("full_name")]
        public string fullName { get; set; }

        [Column("dob")]
        public DateTime dob { get; set; }

        [Column("class_room")]
        public string classRoom { get; set; }

        [Column("major")]
        public string major { get; set; }

        [Column("year_of_study")]
        public int yearOfStudy { get; set; }

        [Column("address")]
        public string address { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("phone_number")]
        public string phoneNumber { get; set; }
    }
}