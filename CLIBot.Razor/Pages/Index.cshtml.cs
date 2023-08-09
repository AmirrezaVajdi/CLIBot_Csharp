using CLIBot.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLIBot.Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(AuthenticationModel command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (command.Email.ToLower().Trim() == "amirreza.vajdy@gmail.com" && command.Password.ToLower().Trim() == "amirrezahack")
                    {
                        if (IsRegistered())
                        {
                            return RedirectToPage("/SelfBot/Management");
                        }
                        else
                        {
                            return RedirectToPage("/SelfBot/Register");
                        }
                    }
                }
                return Page();
            }
            catch (Exception ex)
            {
                return Content(ExceptionHandler.Handler(ex.ToString()));
            }

        }

        private bool IsRegistered()
        {
            if (System.IO.File.Exists("WTelegram.session"))
                return true;
            else
                return false;
        }
    }
}