using System.Net.Mail;

namespace App.Domain.Exceptions
{
    public class EmailSmtpException : Exception
    {
        public EmailSmtpException(SmtpException innerException)
            : base("An error occurred while sending an email.", innerException)
        {
        }
    }
}
