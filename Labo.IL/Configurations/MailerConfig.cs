namespace Labo.IL.Configurations
{
    public class MailerConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TestMail { get; set; } = string.Empty;
    }
}
