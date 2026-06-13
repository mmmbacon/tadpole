namespace Tadpole.Client.Shared.Models;

public sealed record MessageSummary(
    Guid Id,
    Guid ConnectionId,
    Guid SenderChildId,
    string Body,
    DateTimeOffset CreatedAt,
    bool IsMine);
