using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace VirtualCatalogAPI.Businesses.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;

        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["EmailSettings:SmtpHost"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUser = configuration["EmailSettings:SmtpUser"];
            _smtpPassword = configuration["EmailSettings:SmtpPassword"];
            _fromEmail = configuration["EmailSettings:FromEmail"];
        }

        public async Task SendPasswordResetEmail(string email, string token)
        {
            var resetLink = $"http://localhost:5173/virtual-catalog/reset-password?token={token}";
            var subject = "Password Reset Request";
            var body = $@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Password Reset Request</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                color: #333;
                line-height: 1.6;
                margin: 0;
                padding: 0;
            }}
            .email-container {{
                max-width: 600px;
                margin: 20px auto;
                background: #ffffff;
                padding: 20px;
                border: 1px solid #ddd;
                border-radius: 8px;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }}
            .email-header {{
                text-align: center;
                border-bottom: 1px solid #ddd;
                padding-bottom: 10px;
                margin-bottom: 20px;
            }}
            .email-header h1 {{
                font-size: 24px;
                color: #333;
            }}
            .email-body {{
                text-align: left;
            }}
            .reset-link {{
                display: inline-block;
                padding: 10px 20px;
                color: #fff;
                background-color: #007BFF;
                text-decoration: none;
                border-radius: 5px;
                margin-top: 20px;
                text-align: center;
                font-weight: bold;
            }}
            .reset-link:hover {{
                background-color: #0056b3;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
                font-size: 12px;
                color: #aaa;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-header'>
                <h1>Password Reset Request</h1>
            </div>
            <div class='email-body'>
                <p>Hi,</p>
                <p>You requested to reset your password. Please click the button below to reset your password:</p>
                <a href='{resetLink}' class='reset-link'>Reset Password</a>
                <p>This link will expire in 24 hours. If you did not request this, please ignore this email or contact our support team.</p>
                <p>Thank you,</p>
                <p><strong>Virtual Catalog Team</strong></p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 Virtual Catalog. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>";

            using (var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    throw new Exception("Failed to send email.", ex);
                }
            }
        }
    }
}
