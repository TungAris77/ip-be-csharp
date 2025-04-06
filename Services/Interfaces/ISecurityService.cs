namespace iPortal.Services.Interfaces
{
    public interface ISecurityService
    {
        string GetCurrentUsername();
        bool HasRole(string role);
    }
}