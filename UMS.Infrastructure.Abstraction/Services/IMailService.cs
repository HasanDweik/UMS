using _101SendEmailNotificationDoNetCoreWebAPI.Model;

namespace UMS.Infrastructure.Abstraction.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}