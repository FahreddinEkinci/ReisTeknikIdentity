using Microsoft.AspNetCore.Identity;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.Localization;

namespace ReisTeknikIdentity.Web.Extensions
{
    public static class AddIdentityByExtension
    {

        public static void AddIdentityByExtensions(this IServiceCollection services)
        {

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);

            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //  options.User.AllowedUserNameCharacters = "abcdefghiklmnopqrstuvwxyz0123456789_";

                options.User.RequireUniqueEmail = false;

                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<LocalizationIdentityErrorDescripber>()
            .AddPasswordValidator<PasswordValidator>();

        }
    }
}
