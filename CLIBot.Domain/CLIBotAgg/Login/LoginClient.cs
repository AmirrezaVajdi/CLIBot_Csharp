using TL;

namespace CLIBot.Domain.CLIBotAgg
{
    public partial class SelfBotClient
    {
        public async Task Login()
        {
            await SendLoginCode();
        }
        public async Task EnterLoginCode(string loginCode)
        {
            _loginCode = loginCode;
            _Client.Dispose();
            _Client = new(Config);
            await Login();
        }


        private async Task SendLoginCode()
        {
            _ = await LoginUserIfNeeded();
        }
        private async Task<User> LoginUserIfNeeded()
        {
            var user = await _Client.LoginUserIfNeeded();
            return user;
        }

    }
}
