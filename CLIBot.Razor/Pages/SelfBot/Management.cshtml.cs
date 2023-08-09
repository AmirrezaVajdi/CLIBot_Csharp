using CLIBot.Domain.CLIBotAgg;
using CLIBot.Domain.CLIBotAgg.Exceptions;
using CLIBot.Razor.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLIBot.Razor.Pages.SelfBot
{
    public class ManagementModel : PageModel
    {
        public static SelfBotClient client;
        [TempData]
        public string Error { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetStart([FromServices] IWebHostEnvironment env)
        {
            try
            {
                client = new(BotConfig.GetAccount());
                await client.Login();
                await client.StartForward();
                return RedirectToPage("./Management");

            }
            catch (NotFoundChanneIDExcepton ex)
            {
                Error = ex.Message.ToString();
                return RedirectToPage("./Management");

            }

        }
    }
}