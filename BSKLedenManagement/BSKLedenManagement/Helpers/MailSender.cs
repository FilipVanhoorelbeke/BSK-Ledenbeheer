using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using BSKLedenManagement.Models;

namespace BSKLedenManagement.Helpers
{
    public class MailSender
    {
        public static List<string> defaultMailingList = new List<string>
        {
            "filip.vanhoorelbeke@gmail.com"
        };
        public static void SendMail(string subject, string message, string to)
        {
            // send to user's email and sent from a proper email e manneke
            MailMessage mail = new MailMessage("leden@b-s-k.be", to);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
          

            mail.Subject = subject;
            mail.Body = message;
            client.Send(mail);
        }

        public static void SendMultiMail(string subject, string message)
        {
            var tos = new List<string>();
            using (var context = new ApplicationDbContext())
            {
                var users = context.BeheerdersEmails.ToList();
                tos.AddRange(users.Select(user => user.Email));
            }
            if (tos.Count == 0)
            {
                tos.Add("filip.vanhoorelbeke@gmail.com");
            }

            foreach (var to in tos)
            {
                SendMail(subject, message, to);
            }
        }
    }
}