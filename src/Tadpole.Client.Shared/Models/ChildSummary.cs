namespace Tadpole.Client.Shared.Models;

public sealed record ChildSummary(
    Guid Id,
    string DisplayName,
    string Username,
    string FriendCode,
    bool IsActive);
