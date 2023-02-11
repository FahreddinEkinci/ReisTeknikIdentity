namespace ReisTeknikIdentity.Web.Servicess
{
    public interface IEmailService
    {

        public Task SendResetPasswordEmail(string ResetPasswordEmailLink, string ToEmail);
    }
}
