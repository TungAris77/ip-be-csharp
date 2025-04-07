using AutoMapper;
using iPortal.Data.Entities;
using iPortal.DTOs.User;

namespace iPortal.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ CreateSUserRequest sang User
            CreateMap<CreateSUserRequest, User>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.password, opt => opt.Ignore()) // Password được mã hóa riêng trong service
                .ForMember(dest => dest.status, opt => opt.Ignore())   // Status được gán trong service
                .ForMember(dest => dest.roleId, opt => opt.Ignore())   // RoleId được gán trong service
                .ForMember(dest => dest.role, opt => opt.Ignore())     // Role được gán trong service
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua nếu null

            // Ánh xạ từ CreateSUserRequest sang Student
            CreateMap<CreateSUserRequest, Student>()
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.dob, opt => opt.MapFrom(src => src.Dob))
                .ForMember(dest => dest.classRoom, opt => opt.MapFrom(src => src.ClassRoom))
                .ForMember(dest => dest.major, opt => opt.MapFrom(src => src.Major))
                .ForMember(dest => dest.yearOfStudy, opt => opt.MapFrom(src => src.YearOfStudy))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.userId, opt => opt.Ignore())   // UserId được gán sau khi save User
                .ForMember(dest => dest.user, opt => opt.Ignore())     // User được gán trong service
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua nếu null

            // Ánh xạ từ CreateEUserRequest sang User
            CreateMap<CreateEUserRequest, User>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.password, opt => opt.Ignore()) // Password được mã hóa riêng trong service
                .ForMember(dest => dest.status, opt => opt.Ignore())   // Status được gán trong service
                .ForMember(dest => dest.roleId, opt => opt.Ignore())   // RoleId được gán trong service
                .ForMember(dest => dest.role, opt => opt.Ignore())     // Role được gán trong service
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua nếu null

            // Ánh xạ từ CreateEUserRequest sang Employer
            CreateMap<CreateEUserRequest, Employer>()
                .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.userId, opt => opt.Ignore())   // UserId được gán sau khi save User
                .ForMember(dest => dest.user, opt => opt.Ignore())     // User được gán trong service
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua nếu null
        }
    }
}