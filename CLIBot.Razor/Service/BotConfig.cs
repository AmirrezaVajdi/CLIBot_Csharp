using CLIBot.Domain.CLIBotAgg.Login;
using CLIBot.Razor.Models;
using System.Text.Json;
using WTelegram;

namespace CLIBot.Razor.Service
{
    public class BotConfig
    {
        public static Account GetAccount()
        {
            var json = File.ReadAllText("AccountDto.json");
            var account =
            JsonSerializer.Deserialize<Account>(json);
            return account;
        }

        public static void RegisterAccount(Account account)
        {
            string fileName = "AccountDto.json";
            string jsonString = JsonSerializer.Serialize(account);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
