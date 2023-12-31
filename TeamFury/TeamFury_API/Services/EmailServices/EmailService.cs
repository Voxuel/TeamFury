﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Models.Models;

namespace TeamFury_API.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) 
        {
            _config = config;
        }
        public async Task SendEmail(LeaveDays user)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(user.IdentityUser.Email));
                if (user.Request.StatusRequest == StatusRequest.Accepted)
                {
                    email.Subject = user.Request.RequestType.Name;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $"<h1>{user.Request.RequestType.Name} Approved</h1></br>" +
                               $"<p>{user.Request.RequestType.Name} between {user.Request.StartDate:yyyy/MM/dd} and {user.Request.EndDate:yyyy/MM/dd} has been approved</p></br>" +
                               $"<p>Additional comment: {user.Request.MessageForDecline}</p></br><p>Reviewed by {user.Request.AdminName}</p>"

                    };
                }
                else if (user.Request.StatusRequest == StatusRequest.Declined)
                {
                    email.Subject = user.Request.RequestType.Name;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $"<h1>{user.Request.RequestType.Name} Declined</h1> </br>" +
                               $"<p>{user.Request.RequestType.Name} between {user.Request.StartDate:yyyy/MM/dd} and {user.Request.EndDate:yyyy/MM/dd} has been declined</p></br>" +
                               $"<p>Additional comment: {user.Request.MessageForDecline}</p></br><p>Reviewed by {user.Request.AdminName}</p>"

                    };

                }

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.GetSection("EmailUsername").Value,
                    _config.GetSection("EmailPassword").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
