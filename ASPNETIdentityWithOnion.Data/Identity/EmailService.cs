using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Web;
using System.Net.Mime;

namespace ASPNETIdentityWithOnion.Data.Identity
{
    public class EmailService : IIdentityMessageService
    {
       
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.

            return SendGmailAsync(message);           

            //return Task.FromResult(0);
        }

        private Task SendGmailAsync(IdentityMessage message)
        {

            #region formatter
            string text = string.Format(/*"Please click on this link to {0}: {1}", message.Subject,*/ message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            #endregion

            //settings of Login and password
            var from = "elboro.store@gmail.com";
            var passsword = "30elborostore";

            //  address and port of SMTP-server
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, passsword);
            client.EnableSsl = true;

            //create Message (message-Distination)
            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            //mail.Body = message.Body;
            mail.IsBodyHtml = true;
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            return client.SendMailAsync(mail);
        }

    }
}
