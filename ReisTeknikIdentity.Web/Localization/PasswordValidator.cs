using Microsoft.AspNetCore.Identity;
using ReisTeknikIdentity.Web.IdentityModels;

namespace ReisTeknikIdentity.Web.Localization
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {

      readonly  List<IdentityError> errors = new List<IdentityError>();
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            if (password.Contains(user.UserName))
            {
               errors.Add(new IdentityError
                {
                    Code = "PassworkCannotInludeUsername",
                    Description = "Şifreniz kullanıcı adınızı içeremez."

                });
            }

           



            if (errors.Any())
            {
                return await Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return await Task.FromResult(IdentityResult.Success);

        }
    }
}
