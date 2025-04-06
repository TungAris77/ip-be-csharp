using iPortal.DTOs.Common;
using iPortal.DTOs.User;
using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iPortal.Controllers
{
    [Route("api/employer")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IEmployerService _employerService;

        public EmployerController(ISecurityService securityService, IEmployerService employerService)
        {
            _securityService = securityService;
            _employerService = employerService;
        }

        [HttpPost("create")]
        public IActionResult CreateEUser([FromBody] CreateEUserRequest request)
        {
            if (!_securityService.HasRole("MANAGER") && !_securityService.HasRole("ADMIN"))
            {
                return StatusCode(403, new ApiResponse<string>(false, "Access denied", "NO"));
            }

            _employerService.CreateEUser(request);
            return Ok(new ApiResponse<string>(true, "Employer created successfully", "OK"));
        }
    }
}