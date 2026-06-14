using Tadpole.Domain.Entities;

namespace Tadpole.Client.Shared.Models;

public sealed record ConnectionSummary(
    Guid Id,
    Guid ChildLoId,
    Guid ChildHiId,
    string FriendDisplayName,
    ConnectionState State,
    DateTimeOffset CreatedAt);
