using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace Assignment.Utils
{
    public class MailSender
    {
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@JoeStarHotels.com", "JoeStar Hotels");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}