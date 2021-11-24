using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Domain.Util.Mail {
    public interface IEmailSender {

        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendEmailAsync(string email, string subject, string htmlMessage, IList<Attachment> attachments);

    }
}
