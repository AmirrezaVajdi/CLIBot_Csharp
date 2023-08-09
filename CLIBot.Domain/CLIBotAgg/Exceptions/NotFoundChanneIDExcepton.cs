using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIBot.Domain.CLIBotAgg.Exceptions
{
    public class NotFoundChanneIDExcepton : Exception
    {
        public NotFoundChanneIDExcepton(string message = "آیدی عددی کانال ارسال شده صحیح نمی باشد لطفا آیدی صحیح را وارد کنید "):base(message)
        {

        }
    }
}
