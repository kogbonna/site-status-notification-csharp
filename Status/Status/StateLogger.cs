﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilomax.Generic.Infrastructure.Email;
using Bilomax.Generic.Infrastructure.Logging;
using System.Net.Mail;

namespace Status
{
   public static class StateLogger
    {
       public static void Print(string state){
           Console.WriteLine(state);
       }

       public static void SendAlertNotification(IEnumerable<State> stateBatch, string recipients)
       {
           StringBuilder b = new StringBuilder();
            b.AppendLine("The following resources had or have a status change: ");
            stateBatch.ToList().ForEach(
                r => b.AppendLine(String.Format("{0}- Status Code: {1}", r.Url, r.Status)));

            b.AppendLine(DateTime.UtcNow.ToLocalTime().ToString());

           SendMail("", recipients,"Resource Status Alerts",b.ToString());
           
       }

        public static void SendMail(string from, string to, string subject, string body)
        {
        
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            message.To.Add(to);
            
            SmtpClient smtp = new SmtpClient();
            smtp.SendMailAsync(message);
        }
    

    }
}
