namespace Tadpole.Domain.Entities;

public sealed class Child
{
    public Guid Id { get; set; }
    public Guid GuardianId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? PinHash { get; set; }
    public string FriendCode { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsActive { get; set; } = true;

    public Guardian? Guardian { get; set; }
}
