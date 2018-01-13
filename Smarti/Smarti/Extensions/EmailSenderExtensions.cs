using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Smarti.Services;

namespace Smarti.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"10px\" style=\"background-color:#f2f2f2; text-align: center;\"><tr><td><font face=\"'Source Sans Pro', sans-serif\" color=\"#222222\" style=\"font-size: 40px; line-height: 28px;\"><b>SMARTI APP</b></font><td></tr><tr><td><font face=\"'Source Sans Pro', sans-serif\" color=\"#6b7381\" style=\"font-size: 20px; line-height: 28px;\"><br/>Please confirm your email by clicking button bellow</font><td></tr><tr><td><div><a href='{HtmlEncoder.Default.Encode(link)}' style=\"background-color:#337ab7;border:1px solid #337ab7;border-radius:3px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:16px;line-height:44px;text-align:center;text-decoration:none;width:150px;\">Confirm</a></div></td></tr></table>");
        }
    }
}
