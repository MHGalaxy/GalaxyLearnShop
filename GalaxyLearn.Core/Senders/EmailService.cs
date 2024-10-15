using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyLearn.Core.Senders
{
	public interface IEmailService
	{
		Task SendEmailAsync(string to, string subject, string body);
		string GenerateActivationEmail(User user);
		string ResetPasswordEmail(User user);
    }

    public class EmailService : IEmailService
	{
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public async Task SendEmailAsync(string to, string subject, string body)
		{
            string _smtpServer = "smtp.gmail.com";
            int _smtpPort = 587;
            string _smtpUsername = "academygalaxy.ir@gmail.com"; // Replace with your Gmail email address
            string _smtpPassword = "tucb upiv zbml jvgu "; // Replace with your Gmail password

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
			{
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
				client.EnableSsl = true;

				var message = new MailMessage(_smtpUsername, to, subject, body)
				{
					IsBodyHtml = true
				};

				try
				{
					await client.SendMailAsync(message);
				}
				catch (Exception)
				{
				}
			}
		}

        public string GenerateActivationEmail(User user)
        {
            string websiteName = _configuration["WebSiteNames:WebsiteName"];

            // Build the dynamic URL
            string activationUrl = $"{websiteName}/Account/ActiveAccount/{user.ActiveCode}";

            // Create the email body
            var emailBody = new StringBuilder();
            emailBody.AppendLine($"<div style=\"direction: rtl; padding: 20px; font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;\">");
            emailBody.AppendLine($"<h2>فعالسازی حساب کاربری آکادمی گلکسی</h2>");
            emailBody.AppendLine($"<h3>{user.FullName} عزیز !</h3>");
            emailBody.AppendLine($"<h5>با تشکر از ثبت نام شما در آکادمی گلکسی ، جهت ادامه کار میبایست حساب کاربری خود را فعال کنید</h5>");
            emailBody.AppendLine("<br>");
            emailBody.AppendLine($"<p><a style=\"background-color: rgb(0, 150, 0); color: white; border-radius: 0.25rem; text-decoration: none; padding: 1rem; font-weight: bold;\" href=\"{activationUrl}\">فعالسازی حساب کاربری</a></p>");
            emailBody.AppendLine("</div>");

            return emailBody.ToString();
        }

        public string ResetPasswordEmail(User user)
        {
            string websiteName = _configuration["WebSiteNames:WebsiteName"];

            // Build the dynamic URL
            string activationUrl = $"{websiteName}/Account/ResetPassword/{user.ActiveCode}";

            // Create the email body
            var emailBody = new StringBuilder();
            emailBody.AppendLine($"<div style=\"direction: rtl; padding: 20px; font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;\">");
            emailBody.AppendLine($"<h2>بازیابی رمز عبور حساب کاربری آکادمی گلکسی</h2>");
            emailBody.AppendLine($"<h3>{user.FullName} عزیز !</h3>");
            emailBody.AppendLine($"<h5>جهت بازیابی رمز عبور خود روی لینک زیر کلیک کنید</h5>");
            emailBody.AppendLine("<br>");
            emailBody.AppendLine($"<p><a style=\"background-color: rgb(0, 150, 0); color: white; border-radius: 0.25rem; text-decoration: none; padding: 1rem; font-weight: bold;\" href=\"{activationUrl}\">بازیابی رمز عبور</a></p>");
            emailBody.AppendLine("</div>");

            return emailBody.ToString();
        }
    }
}
