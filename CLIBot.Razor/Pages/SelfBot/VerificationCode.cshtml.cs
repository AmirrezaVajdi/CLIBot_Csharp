using CLIBot.Domain.CLIBotAgg;
using CLIBot.Razor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLIBot.Razor.Pages.SelfBot
{
    public class VerificationCodeModel : PageModel
    {
        private static SelfBotClient _client = new(BotConfig.GetAccount());
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string VCode)
        {
            try
            {
                await _client.EnterLoginCode(VCode);

                var user = _client.GetUser();

                if (user is not null)
                {
                    return RedirectToPage("./Management");
                }

                return RedirectToPage("./VerificationCode");
            }
            catch (Exception ex)
            {
                return Content(ExceptionHandler.Handler(ex.ToString()));
            }

        }
    }
}
