namespace ReisTeknikIdentity.Web.Servicess
{
    public class SenderEmailSetting
    {
        // this gets configure from appsettings.json
        public string? Host { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
