using iPortal.DTOs.Common;
using iPortal.DTOs.User;
using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iPortal.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IStudentService _studentService;

        public StudentController(ISecurityService securityService, IStudentService studentService)
        {
            _securityService = securityService;
            _studentService = studentService;
        }

        [HttpPost("create")]
        public IActionResult CreateSUser([FromBody] CreateSUserRequest request)
        {
            if (!_securityService.HasRole("MANAGER") && !_securityService.HasRole("ADMIN"))
            {
                return StatusCode(403, new ApiResponse<string>(false, "Access denied", "NO"));
            }

            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                // Chuyển các lỗi ModelState thành chuỗi thông báo
                var errorMessages = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new ApiResponse<string>(false, "Validation failed", errorMessages));
            }

            // Nếu dữ liệu hợp lệ, tiếp tục tạo sinh viên
            _studentService.CreateSUser(request);

            return Ok(new ApiResponse<string>(true, "Student created successfully", "OK"));
        }

    }
}