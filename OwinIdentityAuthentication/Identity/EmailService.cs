using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OwinIdentityAuthentication.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                if (smtpClient.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
                    smtpClient.EnableSsl = false;

                using (var mailMsg = new MailMessage())
                {
                    mailMsg.To.Add(message.Destination);
                    mailMsg.Subject = message.Subject;
                    mailMsg.Body = message.Body;
                    mailMsg.IsBodyHtml = true;
                    smtpClient.Send(mailMsg);
                }
            }
            return Task.FromResult(true);
        }
    }
}
