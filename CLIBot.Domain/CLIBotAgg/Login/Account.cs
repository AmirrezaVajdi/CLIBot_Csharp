namespace CLIBot.Domain.CLIBotAgg.Login
{
    public class Account
    {
        public string ApiId { get; set; }
        public string ApiHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Password2FA { get; set; }
        public long ChannelId { get; set; }


        public Account(string apiId, string apiHash, string phoneNumber, string password2FA, long channelId)
        {
            ApiId = apiId;
            ApiHash = apiHash;
            PhoneNumber = phoneNumber;
            Password2FA = password2FA;
            ChannelId = channelId;
        }
    }
}
