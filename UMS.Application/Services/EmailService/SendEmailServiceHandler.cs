using _101SendEmailNotificationDoNetCoreWebAPI.Model;
using MailKit;
using MediatR;

namespace UMS.Application.Services;

public class SendEmailServiceHandler:IRequestHandler<SendEmailServiceRequest,bool>
{
    private readonly Infrastructure.Abstraction.Services.IMailService _mailService;

    public SendEmailServiceHandler(Infrastructure.Abstraction.Services.IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task<bool> Handle(SendEmailServiceRequest request, CancellationToken cancellationToken)
    {
        var mailReq = new MailRequest()
        {
            ToEmail = request.ToEmail,
            Subject = request.Subject,
            Body = request.Body
            
        };
        await _mailService.SendEmailAsync(mailReq);
        return true;
    }
}