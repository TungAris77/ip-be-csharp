using iPortal.DTOs.User;

namespace iPortal.Services.Interfaces
{
    public interface IStudentService
    {
        void CreateSUser(CreateSUserRequest request);
    }
}