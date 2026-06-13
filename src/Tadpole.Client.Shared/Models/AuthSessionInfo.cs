namespace Tadpole.Client.Shared.Models;

public enum TadpoleRole
{
    Guardian,
    Child
}

public sealed record AuthSessionInfo(
    string AccessToken,
    TadpoleRole Role,
    Guid UserId,
    string DisplayName);
