using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLIBot.Razor
{
    public class ExceptionHandler
    {
        public static string Handler(string exception)
        {
            return "Error :" + exception.ToString() + "\n" + "با برنامه نویس خطا را در جریان بزارید تا راهنمایی های لازم صورت بگیرد";
        }
    }
}
