using AutoMapper;
using iPortal.Data.Entities;
using iPortal.Data.Repositories;
using iPortal.DTOs.User;
using iPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Services.Implementations
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly RoleRepository _roleRepository;
        private readonly UserRepository _userRepository;
        private readonly StudentRepository _studentRepository;
        private readonly ISecurityService _securityService;

        public StudentServiceImpl(IMapper mapper, RoleRepository roleRepository,
    UserRepository userRepository, StudentRepository studentRepository, ISecurityService securityService)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _securityService = securityService;
        }

        public void CreateSUser(CreateSUserRequest request)
        {
            if (request.Dob > DateTime.Now)
            {
                throw new ArgumentException("Ngày sinh không hợp lệ: không thể ở tương lai");
            }

            var user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); // Sửa ở đây
            var role = _roleRepository.FindByRoleName("STUDENT")
                ?? throw new ArgumentException("Role undefined");
            user.Role = role;
            user.Status = "INACTIVE";
            var savedUser = _userRepository.Save(user);

            var student = _mapper.Map<Student>(request);
            student.User = savedUser;
            _studentRepository.Save(student);
        }
    }
}