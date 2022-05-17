using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarpentryShop.Services;

public interface IEmailSender
{
    Task SendEmailAsync(InternetAddressList emailList, string subject, string htmlMessage);
}

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    public async Task SendEmailAsync(InternetAddressList emailList, string subject, string htmlMessage)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        // mimeMessage.To.Add(MailboxAddress.Parse(email));
        mimeMessage.To.AddRange(emailList);
        mimeMessage.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = htmlMessage };
        // var pathImage = Path.Combine(Path.Combine(Directory.GetCurrentDirectory()), "email_signature.png");
        // var image = builder.LinkedResources.Add(pathImage);
        // image.ContentId = MimeUtils.GenerateMessageId();
        mimeMessage.Body = builder.ToMessageBody();

        try
        {
            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, _emailSettings.UseSsl)
                    .ConfigureAwait(false);

            await client.AuthenticateAsync(_emailSettings.SenderUserName, _emailSettings.Password).ConfigureAwait(false);
            await client.SendAsync(mimeMessage).ConfigureAwait(false);
            await client.DisconnectAsync(true).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }

    }
}
