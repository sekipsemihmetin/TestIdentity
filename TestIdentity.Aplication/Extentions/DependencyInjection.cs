using Microsoft.Extensions.DependencyInjection;
using TestIdentity.Aplication.Services.AuthServices;
using TestIdentity.Aplication.Services.RoleServices;
using TestIdentity.Aplication.Services.TestServices;
using TestIdentity.Aplication.Services.UserServices;

namespace TestIdentity.Aplication.Extentions
{
    /// <summary>
    /// Dependency Injection Extension - Bağımlılık enjeksiyon uzantıları / Dependency injection extensions
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Application servislerini ekler / Adds application services
        /// </summary>
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // Test Services / Test Servisleri
            services.AddScoped<ITestService, TestService>();

            // Auth Services / Kimlik doğrulama servisleri
            services.AddScoped<IAuthService, AuthService>();

            // User Services / Kullanıcı servisleri
            services.AddScoped<IUserService, UserService>();

            // Role Services / Rol servisleri
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
