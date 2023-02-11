using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ReisTeknikIdentity.Web.Servicess
{
    public class EmailService : IEmailService
    {

        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> options)
        {
            _emailSetting = options.Value;
        }
      async Task IEmailService.SendResetPasswordEmail(string ResetPasswordEmailLink, string ToEmail)
        {
            var smptClient = new SmtpClient();
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Host = _emailSetting.Host!;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSetting.Email, _emailSetting.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSetting.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Mail Sıfırlama Linki";
            mailMessage.Body = @$"
                                <h2>Şifrenizi yenilemek için aşağıdaki linke tıklayın.</h2>
                                <p> <a href='{ResetPasswordEmailLink}'>Şifre Yenileme Linki</a> </p> ";

            await smptClient.SendMailAsync(mailMessage);

        }
    }
}
