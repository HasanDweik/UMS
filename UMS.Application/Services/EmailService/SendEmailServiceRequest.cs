using MediatR;

namespace UMS.Application.Services;

public class SendEmailServiceRequest:IRequest<bool>
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}