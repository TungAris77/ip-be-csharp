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

        private string? _phoneNumber;
        [RegularExpression("^0\\d{9,10}$", ErrorMessage = "Số điện thoại phải có 10 hoặc 11 ký tự")]
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = string.IsNullOrEmpty(value) ? null : value;
        }

        [Required(ErrorMessage = "Hãy nhập họ và tên")]
        [StringLength(255, ErrorMessage = "Họ và tên quá dài")]
        [MinLength(4, ErrorMessage = "Họ và tên quá ngắn")]
        public string FullName { get; set; }

        [JsonConverter(typeof(LocalDateConverter))]
        [PastDate(ErrorMessage = "Ngày sinh phải ở quá khứ")]
        public DateTime? Dob { get; set; }

        private string? _classRoom;
        [StringLength(100, ErrorMessage = "Tên lớp học quá dài")]
        [MinLength(5, ErrorMessage = "Tên lớp học quá ngắn")]
        public string? ClassRoom
        {
            get => _classRoom;
            set => _classRoom = string.IsNullOrEmpty(value) ? null : value;
        }

        [Required(ErrorMessage = "Hãy nhập chuyên ngành")]
        [StringLength(100, ErrorMessage = "Chuyên ngành quá dài")]
        public string Major { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Year of study must be at least 1")]
        public int YearOfStudy { get; set; }

        private string? _address;
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Địa chỉ chưa cụ thể")]
        public string? Address
        {
            get => _address;
            set => _address = string.IsNullOrEmpty(value) ? null : value;
        }

        private string? _email;
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(255, ErrorMessage = "Email quá dài")]
        public string? Email
        {
            get => _email;
            set => _email = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}