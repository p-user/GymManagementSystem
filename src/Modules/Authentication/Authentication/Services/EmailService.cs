namespace Authentication.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string MailServer = configuration["EmailSettings:MailServer"];
            string FromEmail = configuration["EmailSettings:FromEmail"];
            string Password = configuration["EmailSettings:Password"];
            int Port = int.Parse(configuration["EmailSettings:MailPort"]);
            var client = new SmtpClient(MailServer, Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail, Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            MailMessage mailMessage = new MailMessage(FromEmail, email, subject, message)
            {
                IsBodyHtml = false
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
