using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Configuration;

namespace FinanceManager.Services
{
    public class MailerService
    {
        public static object SendEmail(string to, string subject, string message)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = ConfigurationManager.AppSettings["mailerUsername"],
                Password = ConfigurationManager.AppSettings["mailerPassword"]
            };
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage("vasilita.kiril@gmail.com", to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            object tmp = new object();
            smtpClient.Send(mailMessage);
            tmp = new { message = "message sent", code = 200 };

            return tmp;
        }
    }
}