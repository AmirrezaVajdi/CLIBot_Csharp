namespace CLIBot.Razor.Models
{
    public class AccountDto
    {
        public string ApiId { get; set; }
        public string ApiHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Password2FA { get; set; }
        public long ChannelId { get; set; }
    }
}
