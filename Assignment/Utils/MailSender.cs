﻿using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
            var bytes = File.ReadAllBytes(@"C:\Users\aadit\Desktop\Monash Sem 3\FIT5032\Assessment_Task_1.pdf");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("ABC.pdf", file);
            var response = client.SendEmailAsync(msg);

            
        }



        public void SendMultiple(List<EmailAddress> toEmails, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@JoeStarHotels.com", "JoeStar Hotels");
            //var to = new EmailAddress(toEmail, "");
            
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toEmails, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
        
        
    }
}