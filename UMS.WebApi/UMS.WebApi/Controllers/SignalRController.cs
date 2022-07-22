using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SignalRController : Controller
{
    [HttpPost("Send")]
    public async Task<IActionResult> SendMessage(String message)
    {
        MessageHub _messageHub = new MessageHub();
       await _messageHub.SendMessage(message);
        return Ok();
    }
}