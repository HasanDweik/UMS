using _101SendEmailNotificationDoNetCoreWebAPI.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Services;
using UMS.Infrastructure.Abstraction.Services;

namespace UMS.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : Controller
{
    private readonly IMediator _mediator;
    public MailController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm]SendEmailServiceRequest request)
    {
        return Ok( await _mediator.Send(request));
    }
}