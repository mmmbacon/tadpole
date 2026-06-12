namespace Tadpole.Domain.Entities;

public enum ConnectionState
{
    Requested,
    GuardianLoApproved,
    Active,
    Revoked
}

public sealed class Connection
{
    public static (Guid Lo, Guid Hi) CanonicalizeChildPair(Guid childA, Guid childB)
    {
        if (childA == childB)
            throw new ArgumentException("A child cannot connect to themselves.", nameof(childB));

        return childA.CompareTo(childB) < 0 ? (childA, childB) : (childB, childA);
    }

    public Guid Id { get; set; }
    public Guid ChildLoId { get; set; }
    public Guid ChildHiId { get; set; }
    public ConnectionState State { get; set; } = ConnectionState.Requested;
    public Guid? InitiatedByChildId { get; set; }
    public DateTimeOffset? GuardianLoApprovedAt { get; set; }
    public DateTimeOffset? GuardianHiApprovedAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public Guid? RevokedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
