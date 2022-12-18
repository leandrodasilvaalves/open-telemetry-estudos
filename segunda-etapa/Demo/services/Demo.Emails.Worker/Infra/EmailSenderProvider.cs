using System.Net;
using System.Net.Mail;
using Demo.Emails.Worker.Config;
using Demo.Emails.Worker.Models;
using Microsoft.Extensions.Options;

namespace Demo.Emails.Worker.Infra
{
    public interface IEmailSenderProvider
    {
        Task SendAsync(Email email);
    }

    public class EmailSenderProvider : IEmailSenderProvider
    {
        private readonly EmailOptions _options;

        public EmailSenderProvider(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(Email email)
        {
            var client = new SmtpClient(_options.Smtp, _options.Port)
            {
                Credentials = new NetworkCredential(_options.UserName, _options.Password),
                EnableSsl = _options.EnableSsl,
            };
            client.Send(_options.EmaiFrom, email.To, email.Subject, email.Body);
            await Task.CompletedTask;
        }
    }
}