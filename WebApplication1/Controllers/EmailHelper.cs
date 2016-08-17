using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmailHelper
    {
        private const String ADMIN_GMAIL_EMAIL = "trananhkhoi1996@gmail.com";
        private const String ADMIN_GMAIL_EMAIL_PASSWORD = "5560020123aA";

        private static EmailHelper instance;
        public static EmailHelper getInstance()
        {
            if (instance == null)
            {
                instance = new EmailHelper();
            }
            return instance;
        }
        
        public string getBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        public void sendPasswordRecoveryMail(string sendToEmail, string password)
        {
            if (!password.Equals(""))
            {
                sendMail(ADMIN_GMAIL_EMAIL, ADMIN_GMAIL_EMAIL_PASSWORD, sendToEmail, "Password recovery",
                    "Recovering the password: " + password);
            }
        }

        public void sendActivatingMail(string sendToEmail)
        {
            sendMail(ADMIN_GMAIL_EMAIL, ADMIN_GMAIL_EMAIL_PASSWORD, sendToEmail, "Activate Account",
                 "Click this URL to activate your account: " +
                getBaseUrl() + "Home/ActivateMemberAccount?email=" + sendToEmail);
        }

        public void sendMail(string sendFromEmail, string sendFromPassword, string sendToEmail, string subject, string messageBody)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587; // Gmail works on this port

            mail.From = new MailAddress(sendFromEmail);
            mail.To.Add(sendToEmail);
            mail.Subject = subject;
            mail.Body = messageBody;
            mail.Priority = MailPriority.High;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new System.Net.NetworkCredential(sendFromEmail, sendFromPassword);
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
        }
    }
}