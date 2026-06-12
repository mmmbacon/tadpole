using Tadpole.Domain.Entities;

namespace Tadpole.Application.Abstractions;

public interface IMessageService
{
    Task<Message> SendMessageAsync(Guid senderChildId, Guid recipientChildId, string body, CancellationToken cancellationToken = default);
}
