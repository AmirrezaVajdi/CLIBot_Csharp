using CLIBot.Domain.CLIBotAgg;
using CLIBot.Domain.CLIBotAgg.Login;
using CLIBot.Razor.Models;
using CLIBot.Razor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CLIBot.Razor.Pages.SelfBot
{
    public class RegisterModel : PageModel
    {
        private SelfBotClient _client;

        public void OnGet()
        {
        }

        public IActionResult OnPost(AccountDto command)
        {
            try
            {
                Account account = new(command.ApiId, command.ApiHash, command.PhoneNumber, command.Password2FA, command.ChannelId);
                BotConfig.RegisterAccount(account);
                _client = new(account);
                _client.Login();

                return RedirectToPage("./VerificationCode");
            }
            catch (Exception ex)
            {
                return Content(ExceptionHandler.Handler(ex.ToString()));
            }
        }
    }
}

