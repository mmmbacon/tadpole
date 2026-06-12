namespace Tadpole.Domain.Entities;

public sealed class Message
{
    public Guid Id { get; set; }
    public Guid ConnectionId { get; set; }
    public Guid SenderChildId { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeliveredAt { get; set; }
    public DateTimeOffset? ReadAt { get; set; }
}
