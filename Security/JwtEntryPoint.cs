using Microsoft.AspNetCore.Http;

namespace iPortal.Security
{
    public class JwtEntryPoint
    {
        public async Task Commence(HttpRequest request, HttpResponse response, Exception authException)
        {
            response.StatusCode = StatusCodes.Status401Unauthorized;
            await response.WriteAsync(authException.Message);
        }
    }
}