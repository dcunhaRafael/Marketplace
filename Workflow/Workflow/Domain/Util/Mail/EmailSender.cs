using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Domain.Util.Mail {
    public class EmailSender : IEmailSender {
        private EmailSettings emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings) {
            this.emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage) {
            try {
                ExecuteSendEmailAsync(email, subject, htmlMessage, new List<Attachment>()).Wait();
                return Task.FromResult(0);
            } catch (Exception) {
                throw;
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage, IList<Attachment> attachments) {
            try {
                ExecuteSendEmailAsync(email, subject, htmlMessage, attachments).Wait();
                return Task.FromResult(0);
            } catch (Exception) {
                throw;
            }
        }

        private async Task ExecuteSendEmailAsync(string email, string subject, string htmlMessage, IList<Attachment> attachments) {
            try {

                MailMessage mail = new() {
                    From = new MailAddress(emailSettings.UserEmail, emailSettings.UserName)
                };

                var emailList = email.Split(";");
                foreach (var item in emailList) {
                    mail.To.Add(new MailAddress(item));
                }
                mail.Subject = subject;
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;

                foreach (var attachment in attachments) {
                    mail.Attachments.Add(attachment);
                }

                using SmtpClient smtp = new(emailSettings.Host, emailSettings.Port);
                smtp.Credentials = new NetworkCredential(emailSettings.UserEmail, emailSettings.UserPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            } catch (Exception) {
                throw;
            }
        }

    }
}
