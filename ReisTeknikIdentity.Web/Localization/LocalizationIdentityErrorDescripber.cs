using Microsoft.AspNetCore.Identity;

namespace ReisTeknikIdentity.Web.Localization
{
    public class LocalizationIdentityErrorDescripber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} daha önce kayıt edildi, lütfen farklı bir Kullanıcı Adı deneyiniz"
            };
            //return base.DuplicateUserName(userName);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "DuplicateEmail",
                Description = $"{email} önce kayıt edildi. farklı bir E-posta deneyiniz"
            };
            /*  return base.DuplicateEmail(email); */
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = "Şifre Çok kısa min 6 karakter olmalı"
            };
           
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresLower",
                Description = "Şifre en az bir küçük karakter içermelidir."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "PasswordRequiresUpper",
                Description = "Şifre en az bir büyük karakter içermelidir. "
            };
        }

        
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {

                Code = "UserNotInRole",
                Description = "bunu yapmaya yetkiniz yok"
            };
         
        }




    }
}
