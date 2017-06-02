using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Threading;
namespace Angular2App.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("Администрация cервиса", "transportation.service@outlook.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp-mail.outlook.com", 587, false);
                await client.AuthenticateAsync("transportation.service@outlook.com", "diawestactive95");
                await client.SendAsync(emailMessage);
 
                await client.DisconnectAsync(true);
            }
        }
    }
}