using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureProject.Shared.Services;

public static class SendGridService
{
    public static async Task SendEmail(string fromEmail, string fromName, string subject, string toEmail,
        string toName, string plainTextContent, string? htmlContent)
    {
        var apiKey = Environment.GetEnvironmentVariable(KeyVaultEnum.SendGridKey.ToString()) ?? string.Empty;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(toEmail, toName);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}


//https://app.sendgrid.com/guide/integrate/langs/csharp