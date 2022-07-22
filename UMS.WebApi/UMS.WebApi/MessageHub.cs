
using Microsoft.AspNetCore.SignalR;

namespace UMS.WebApi;

public class MessageHub : Hub
{
    public async Task SendMessage(string message)
    {
            await Clients.All.SendAsync("ReceiveMessageHandler", message);
    }
}