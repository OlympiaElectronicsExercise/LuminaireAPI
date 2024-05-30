namespace API.Models
{
    public class EmailConfigurationModel
    {
        public string? From { get; set; } = "noreply@luminaire.com";
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool EnableSsl { get; set; } = false;
        public string? SenderName { get; set; } = "no-reply | Luminaire API";
    }
}