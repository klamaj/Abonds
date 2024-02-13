using System.Text;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServiceExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<UserModel>(options => 
            {
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
                options.Tokens.AuthenticatorIssuer = "JWT";
                // options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<RoleModel>()
            .AddRoleManager<RoleManager<RoleModel>>()
            .AddSignInManager<SignInManager<UserModel>>()
            .AddRoleValidator<RoleValidator<RoleModel>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            // Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            return services;
        }
    }
}