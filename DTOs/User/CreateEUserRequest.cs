using System.ComponentModel.DataAnnotations;

namespace iPortal.DTOs.User
{
    public class CreateEUserRequest
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

        [Required(ErrorMessage = "Hãy nhập tên công ty")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "Tên công ty không hợp lệ")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Địa chỉ chưa cụ thể")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hãy nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(255, ErrorMessage = "Email quá dài")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hãy nhập địa chỉ website")]
        [MinLength(10, ErrorMessage = "Địa chỉ website không hợp lệ")]
        public string Website { get; set; }
    }
}