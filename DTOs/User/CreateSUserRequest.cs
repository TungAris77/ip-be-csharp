using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using iPortal.Utils;

namespace iPortal.DTOs.User
{
    public class CreateSUserRequest
    {
        [Required(ErrorMessage = "Hãy nhập username")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Username phải có 9 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hãy nhập số điện thoại")]
        [RegularExpression("^0\\d{9,10}$", ErrorMessage = "Số điện thoại phải có 10 hoặc 11 ký tự")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Hãy nhập họ và tên")]
        [StringLength(255, ErrorMessage = "Họ và tên quá dài")]
        public string FullName { get; set; }

        [JsonConverter(typeof(LocalDateConverter))]
        [PastDate(ErrorMessage = "Ngày sinh phải ở quá khứ")]
        public DateTime Dob { get; set; }

        [StringLength(100, ErrorMessage = "Tên lớp học quá dài")]
        public string ClassRoom { get; set; }

        [Required(ErrorMessage = "Hãy nhập chuyên ngành")]
        [StringLength(100, ErrorMessage = "Chuyên ngành quá dài")]
        public string Major { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year of study must be at least 1")]
        public int YearOfStudy { get; set; }

        [StringLength(500, ErrorMessage = "Địa chỉ quá dài")]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(255, ErrorMessage = "Email quá dài")]
        public string Email { get; set; }
    }
}