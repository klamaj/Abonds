namespace Core.Models.EmailModels
{
    public class EmailConfigurationModel
    {
        public string? From { get; set; } = "noreply@ferye.com";
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}