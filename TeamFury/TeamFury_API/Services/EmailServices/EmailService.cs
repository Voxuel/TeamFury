using MailKit.Net.Smtp;
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
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(user.IdentityUser.Email));
            if (user.Request.StatusRequest == StatusRequest.Accepted)
            {
                email.Subject = user.Request.RequestType.Name;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<h1>{user.Request.RequestType.Name} Aproved</h1>"
                };
            }
            else if (user.Request.StatusRequest == StatusRequest.Declined)
            {
                email.Subject = user.Request.RequestType.Name;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"<h1>{user.Request.RequestType.Name} Declined</h1>"
                };

            }
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
