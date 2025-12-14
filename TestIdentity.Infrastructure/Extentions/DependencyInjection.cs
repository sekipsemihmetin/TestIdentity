using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestIdentity.Domain.Entities;
using TestIdentity.Infrastructure.AppContext;
using TestIdentity.Infrastructure.Repositories.TestRepositories;

namespace TestIdentity.Infrastructure.Extentions
{
    /// <summary>
    /// Dependency Injection Extension - Bağımlılık enjeksiyon uzantıları / Dependency injection extensions
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Infrastructure servislerini ekler / Adds infrastructure services
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context / Veritabanı bağlamı
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseLazyLoadingProxies();
                option.UseSqlServer(configuration.GetConnectionString(AppDbContext.DevConnectionString), 
                    sqlServerOptions =>
                    {
                        // Retry sadece gerçekten geçici hatalar için (deadlock, timeout vb.)
                        // Retry only for truly transient errors (deadlock, timeout, etc.)
                        sqlServerOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null);
                        sqlServerOptions.CommandTimeout(60); // 60 saniye command timeout
                    });
                // Sensitive data logging - sadece development için / only for development
                option.EnableSensitiveDataLogging();
                option.EnableDetailedErrors();
            });

            // Identity Configuration / Kimlik yapılandırması
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password settings / Şifre ayarları
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings / Kilitlenme ayarları
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings / Kullanıcı ayarları
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // JWT Authentication / JWT Kimlik doğrulama
            var jwtKey = configuration["Jwt:Key"] ?? "";
            var jwtIssuer = configuration["Jwt:Issuer"] ?? "";
            var jwtAudience = configuration["Jwt:Audience"] ?? "";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Repositories / Depolar
            services.AddScoped<ITestRepository, TestRepository>();

            return services;
        }
    }
}
