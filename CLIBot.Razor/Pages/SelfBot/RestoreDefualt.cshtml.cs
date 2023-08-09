using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLIBot.Razor.Pages.SelfBot
{
    public class RestoreDefualtModel : PageModel
    {
        public IActionResult OnGet()
        {
            try
            {
                ManagementModel.client.Dispose();
                if (System.IO.File.Exists("WTelegram.session"))
                {
                    System.IO.File.Delete("WTelegram.session");
                }
                if (System.IO.File.Exists("AccountDto.json"))
                {
                    System.IO.File.Delete("AccountDto.json");
                }
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                return Content(ExceptionHandler.Handler(ex.ToString()));
            }

        }
    }
}
