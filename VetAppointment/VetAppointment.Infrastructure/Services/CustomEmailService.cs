﻿using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace VetAppointment.Infrastructure.Services
{
    public class CustomEmailService
    {
        private readonly IConfiguration appSettings;

        public CustomEmailService(IConfiguration appSettings)
        {
            this.appSettings = appSettings;
        }

        public void Send(string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(appSettings["EmailConfiguration:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(appSettings["EmailConfiguration:SmtpServer"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(appSettings["EmailConfiguration:Username"], appSettings["EmailConfiguration:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        /****************
         * EMAIL TEMPLATES
         ****************/
        public void SendPayment(string to, string clientName, string appointmentId)
        {
            string subject = "Appointment Created";
            string body =
                string.Format(
                    "<h1>Hi {0}</h1>. " +
                    "<p>An appointment was created for you." +
                    " Please use the following link for payment.</p> " +
                    "<a target='_blank' href='http://localhost:3000/payment?id={1}'>Pay here</a>",
                    clientName, appointmentId);
        }
    }
}