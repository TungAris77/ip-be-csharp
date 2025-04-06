using iPortal.DTOs.User;

namespace iPortal.Services.Interfaces
{
    public interface IEmployerService
    {
        void CreateEUser(CreateEUserRequest request);
    }
}