using AutoMapper;
using iPortal.Data.Entities;
using iPortal.Data.Repositories;
using iPortal.DTOs.User;
using iPortal.Services.Interfaces;

namespace iPortal.Services.Implementations
{
    public class EmployerServiceImpl : IEmployerService
    {
        private readonly IMapper _mapper;
        private readonly RoleRepository _roleRepository;
        private readonly UserRepository _userRepository;
        private readonly EmployerRepository _employerRepository;

        public EmployerServiceImpl(IMapper mapper, RoleRepository roleRepository,
            UserRepository userRepository, EmployerRepository employerRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _employerRepository = employerRepository;
        }

        public void CreateEUser(CreateEUserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.password = BCrypt.Net.BCrypt.HashPassword(request.Password); // Đổi từ Password
            var role = _roleRepository.FindByRoleName("EMPLOYER")
                ?? throw new ArgumentException("Role undefined");
            user.role = role; // Đổi từ Role
            user.status = "INACTIVE"; // Đổi từ Status
            var savedUser = _userRepository.Save(user);

            var employer = _mapper.Map<Employer>(request);
            employer.user = savedUser; // Đổi từ User
            _employerRepository.Save(employer);
        }
    }
}