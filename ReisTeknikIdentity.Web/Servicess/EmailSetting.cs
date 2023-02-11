namespace ReisTeknikIdentity.Web.Servicess
{
    public class EmailSetting
    {
        public EmailSetting()
        {

        }

        public EmailSetting(string? host, string? email, string? password)
        {
            Host = host;
            Email = email;
            Password = password;
        }

        // this gets configure from appsettingsDevelopment.json
        public string? Host { get; set; }
        public string? Email { get; set; }
        public string?  Password { get; set; }
    }
}
