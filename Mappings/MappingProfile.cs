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
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // Password sẽ được mã hóa riêng
                .ForMember(dest => dest.Status, opt => opt.Ignore())   // Status được gán trong service
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())   // RoleId được gán trong service
                .ForMember(dest => dest.Role, opt => opt.Ignore());    // Role được gán trong service

            // Ánh xạ từ CreateSUserRequest sang Student
            CreateMap<CreateSUserRequest, Student>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
                .ForMember(dest => dest.ClassRoom, opt => opt.MapFrom(src => src.ClassRoom))
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major))
                .ForMember(dest => dest.YearOfStudy, opt => opt.MapFrom(src => src.YearOfStudy))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())   // UserId được gán sau khi save User
                .ForMember(dest => dest.User, opt => opt.Ignore());    // User được gán trong service

            // Ánh xạ từ CreateEUserRequest sang User
            CreateMap<CreateEUserRequest, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // Password được mã hóa riêng
                .ForMember(dest => dest.Status, opt => opt.Ignore())   // Status được gán trong service
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())   // RoleId được gán trong service
                .ForMember(dest => dest.Role, opt => opt.Ignore());    // Role được gán trong service

            // Ánh xạ từ CreateEUserRequest sang Employer
            CreateMap<CreateEUserRequest, Employer>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())   // UserId được gán sau khi save User
                .ForMember(dest => dest.User, opt => opt.Ignore());    // User được gán trong service
        }
    }
}