using iPortal.Data.Repositories;
using iPortal.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace iPortal.Config
{
    public class SecurityConfig
    {
        private readonly UserRepository _userRepository;
        private readonly CustomUserDetailsService _userDetailsService;
        private readonly JwtEntryPoint _entryPoint;
        private readonly JwtFilter _jwtFilter;

        public SecurityConfig(UserRepository userRepository, CustomUserDetailsService userDetailsService,
            JwtEntryPoint entryPoint, JwtFilter jwtFilter)
        {
            _userRepository = userRepository;
            _userDetailsService = userDetailsService;
            _entryPoint = entryPoint;
            _jwtFilter = jwtFilter;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "https://ip-fe.vercel.app")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtFilter>();
        }
    }
}