using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
// using SendGrid;
// using SendGrid.Helpers.Mail;
using TodoBackend.Api.Data.ViewModels;
using TodoBackend.Api.Interfaces;

namespace TodoBackend.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("Auth:Email:ApiKey");
        }

        public Task SendEmail(UserViewModel userTo, UserViewModel userFrom, string subject, string plainTextContent, string htmlContent)
        {
            // var client = new SendGridClient(_apiKey);
            // var from_email = new EmailAddress(userFrom.Email, userFrom.UserName);
            // var to_email = new EmailAddress(userTo.Email, userTo.UserName);
            // var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
            // var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            throw new System.NotImplementedException();
        }
    }
}