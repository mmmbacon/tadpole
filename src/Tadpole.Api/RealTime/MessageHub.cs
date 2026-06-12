using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Tadpole.Api.RealTime;

[Authorize]
public sealed class MessageHub : Hub
{
    public async Task SendMessage(Guid recipientId, string body)
    {
        // Placeholder: in real implementation, validate and persist
        await Clients.User(recipientId.ToString())
            .SendAsync("ReceiveMessage", new { body, sentAt = DateTimeOffset.UtcNow });
    }
}
